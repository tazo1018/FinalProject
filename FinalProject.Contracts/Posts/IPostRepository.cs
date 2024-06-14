using FinalProject.Entities;
using System.Linq.Expressions;

namespace FinalProject.Contracts.Posts
{
    public interface IPostRepository : ISavable
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetAllPostsAsync(Expression<Func<Post, bool>> filter);
        Task<Post?> GetSinglePostAsync(Expression<Func<Post, bool>> filter);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        void DeletePost(Post post);

    }
}
