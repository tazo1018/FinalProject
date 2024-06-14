using FinalProject.Contracts.Comments;
using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        if (comment != null)
        {
            await _context.Comments.AddAsync(comment);
        }
    }

    public void DeleteComment(Comment comment)
    {
        if (comment != null)
        {
            _context.Comments.Remove(comment);
        }
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<List<Comment>> GetAllCommentsAsync(Expression<Func<Comment, bool>> filter)
    {
        return await _context.Comments
            .Where(filter)
            .ToListAsync();
    }

    public async Task<Comment> GetSingleCommentAsync(Expression<Func<Comment, bool>> filter)
    {
        return await _context.Comments.FirstOrDefaultAsync(filter);
    }

    public async Task Save() => await _context.SaveChangesAsync();

    public async Task UpdateCommentAsync(Comment comment)
    {
        if (comment != null)
        {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

            if (commentToUpdate != null)
            {
                commentToUpdate.Text = comment.Text;

                _context.Comments.Update(commentToUpdate);

            }

        }
    }
}
