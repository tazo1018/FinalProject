using FinalProject.Models.Comment;

namespace FinalProject.Contracts.Comments
{
    public interface ICommentService
    { 
        Task<List<CommentForGettingDTO>> GetAllCommentsAsync();
        Task<CommentForGettingDTO> GetSingleCommentAsync(int id);
        Task AddCommentAsync(CommentForCreatingDTO model);
        Task UpdateCommentAsync(CommentForUpdatingDTO model);
        Task DeleteCommentAsync(int id);
    }
}
