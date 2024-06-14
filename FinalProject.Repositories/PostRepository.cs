using FinalProject.Contracts.Posts;
using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPostAsync(Post post)
        {
            if (post != null)
            {
                await _context.Posts.AddAsync(post);
            }
        }

        public void DeletePost(Post post)
        {
            if(post != null)
            {
                _context.Posts.Remove(post);
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(x => x.Comments).ToListAsync();
        }

        public async Task<List<Post>> GetAllPostsAsync(Expression<Func<Post, bool>> filter)
        {
            return await _context.Posts
                .Where(filter)
                .ToListAsync();    
        }

        public async Task<Post> GetSinglePostAsync(Expression<Func<Post, bool>> filter)
        {
            return await _context.Posts.Include(x => x.Comments).FirstOrDefaultAsync(filter);
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public async Task UpdatePostAsync(Post post)
        {
            if (post != null)
            {
                var postToUpdate = await _context.Posts.FirstOrDefaultAsync(x => x.Id == post.Id);

                if(postToUpdate != null)
                {
                    postToUpdate.Title = post.Title;
                    postToUpdate.Status = post.Status;
                    postToUpdate.State = post.State;
                    postToUpdate.Description = post.Description;

                    _context.Posts.Update(postToUpdate);

                }

            }
        }
    }
}
