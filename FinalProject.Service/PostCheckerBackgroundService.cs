using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class PostCheckerBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public PostCheckerBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckPostStatus, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void CheckPostStatus(object state)
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

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
