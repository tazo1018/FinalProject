using AutoMapper;
using FinalProject.Contracts.Comments;
using FinalProject.Contracts.Posts;
using FinalProject.Entities;
using FinalProject.Models.Comment;
using FinalProject.Service.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Service.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPostRepository _postRepository;
    public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IPostRepository postRepository)
    {
        // aq ewrera _httpContextAccessor = _httpContextAccessor;
        _commentRepository = commentRepository;
        _mapper = MappingInitializer.Initialize();
        _httpContextAccessor = httpContextAccessor;
        _postRepository = postRepository;
    }

    public async Task AddCommentAsync(CommentForCreatingDTO model)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;

        if (model is null)
        {
            throw new ArgumentNullException("invalid argument");
        }
        var posit = await _postRepository.GetSinglePostAsync(x => x.Id == model.PostId &&  x.Status == Status.Active);
        if (posit == null)
        {
            throw new PostNotFoundException();
        }

        var result = _mapper.Map<Comment>(model); // aq wesit egrea
        result.UserId = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        result.UploadTime = DateTime.Now;
        await _commentRepository.AddCommentAsync(result);
        await _commentRepository.Save();

    }

    public async Task DeleteCommentAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("invalid argument");
        }

        var result = await _commentRepository.GetSingleCommentAsync(x => x.Id == id);

        if (result is null)
        {
            throw new CommentNotFoundException();
        }

        _commentRepository.DeleteComment(result);
        await _commentRepository.Save();
    }

    public async Task<List<CommentForGettingDTO>> GetAllCommentsAsync()
    {
        var rows = await _commentRepository.GetAllCommentsAsync();

        if (rows.Count == 0)
        {
            throw new CommentNotFoundException();
        }

        var result = _mapper.Map<List<CommentForGettingDTO>>(rows);
        return result;
    }


    public async Task<CommentForGettingDTO> GetSingleCommentAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("invalid argument passed");
        }
        var row = await _commentRepository.GetSingleCommentAsync(x => x.Id == id);

        if (row is null)
        {
            throw new CommentNotFoundException();
        }

        var result = _mapper.Map<CommentForGettingDTO>(row);
        return result;
    }

    public async Task UpdateCommentAsync(CommentForUpdatingDTO model)
    {
        if (model is null)
        {
            throw new ArgumentNullException("invalid argument");
        }

        var result = _mapper.Map<Comment>(model);
        await _commentRepository.UpdateCommentAsync(result);
        await _commentRepository.Save();
    }
}
