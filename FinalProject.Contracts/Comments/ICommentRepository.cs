using FinalProject.Entities;
using System.Linq.Expressions;

namespace FinalProject.Contracts.Comments
{
    public interface ICommentRepository : ISavable
    {
        Task<List<Comment>> GetAllCommentsAsync();
        //raghac pirobis mixedvit wamogheba
        Task<List<Comment>> GetAllCommentsAsync(Expression<Func<Comment, bool>> filter);
        Task<Comment> GetSingleCommentAsync(Expression<Func<Comment, bool>> filter);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        void DeleteComment(Comment comment);
    }
}
