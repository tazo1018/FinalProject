using AutoMapper;
using FinalProject.Contracts.Posts;
using FinalProject.Entities;
using FinalProject.Models.Comment;
using FinalProject.Models.Post;
using FinalProject.Service.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Service.Implementations
{

    public class PostService : IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
        {
            // aq ewrera _httpContextAccessor = _httpContextAccessor;
            _postRepository = postRepository;
            _mapper = MappingInitializer.Initialize();
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task AddPostsAsync(PostForCreatingDTO model)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            if (model is null)
            {
                throw new ArgumentNullException("invalid argument");
            }

            var result = _mapper.Map<Post>(model); // aq wesit egrea\
            result.AuthorId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            result.Status = Status.Active;
            result.State = State.Pending;
            result.CreationTime = DateTime.Now;
            await _postRepository.AddPostAsync(result);
            await _postRepository.Save();

        }

        public async Task ChangePostsStateAsync(PostForChangingStateDTO model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid argument passed");
            }
            var row = await _postRepository.GetSinglePostAsync(x => x.Id == model.Id);

            if (row is null)
            {
                throw new PostNotFoundException();
            }

            row.State = model.State;
            await _postRepository.UpdatePostAsync(row);
            await _postRepository.Save();
        }

        public async Task ChangePostsStatusAsync(PostForChangingStatusDTO model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException("invalid argument passed");
            }
            var row = await _postRepository.GetSinglePostAsync(x => x.Id == model.Id);

            if (row is null)
            {
                throw new PostNotFoundException();
            }

            row.Status = model.Status;
            await _postRepository.UpdatePostAsync(row);
            await _postRepository.Save();
        }

        public async Task DeletePostAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid argument");
            }
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var authorId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var result = await _postRepository.GetSinglePostAsync(x => x.Id == id && x.AuthorId == authorId);

            if (result is null)
            {
                throw new PostNotFoundException();
            }

            _postRepository.DeletePost(result);
            await _postRepository.Save();
        }

        public async Task<List<PostForGettingDTO>> GetAllPostsAsync()
        {
            var rows = await _postRepository.GetAllPostsAsync();

            if (rows.Count == 0)
            {
                throw new PostNotFoundException();
            }

            var result = rows.Select(row =>
            {
                var dto = _mapper.Map<PostForGettingDTO>(row);
                dto.NumberOfComments = row.Comments.Count();
                return dto;
            }).ToList();

            return result;
        }

        public async Task<PostForGettingDetailedDTO> GetSinglePostsAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid argument passed");
            }
            var row = await _postRepository.GetSinglePostAsync(x => x.Id == id);

            if (row is null)
            {
                throw new PostNotFoundException();
            }

            var result = _mapper.Map<PostForGettingDetailedDTO>(row);
            result.Comments = _mapper.Map<List<CommentForGettingDTO>>(row.Comments);
            return result;
        }

        public async Task UpdatePostsAsync(PostForUpdatingDTO model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("invalid argument");
            }
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var authorId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            
            var result = await _postRepository.GetSinglePostAsync(x => x.Id == model.Id && x.AuthorId == authorId);
            
            if (result is null)
            {
                throw new PostNotFoundException();
            }

            result.Title = model.Title;
            result.Description = model.Description;

            await _postRepository.UpdatePostAsync(result);
            await _postRepository.Save();
        }
    }
}
