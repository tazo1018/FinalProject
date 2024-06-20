using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinalProject.Service;

public class PostCheckerBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PostCheckerBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    private async Task CheckPostStatus()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var xDays = 5;
            var now = DateTime.Now;

            var posts = await context.Posts.Include(x => x.Comments)
                .Where(p => p.Status != Status.InActive).ToListAsync();

            var postToUpdate = posts.Where(x => CheckLastComment(x, now, xDays));
            foreach (var post in postToUpdate)
            {
                post.Status = Status.InActive;
            }

            await context.SaveChangesAsync();
        }
    }


    private bool CheckLastComment(Post post, DateTime now, int xday)
    {
        if (post.Comments.Any())
        {
            var lastComment = post.Comments.OrderByDescending(x => x.UploadTime).First();
            return (now - lastComment.UploadTime).TotalDays > xday;
        }
        return false;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckPostStatus();
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}