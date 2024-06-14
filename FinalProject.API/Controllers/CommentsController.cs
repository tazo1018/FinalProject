using FinalProject.Contracts.Comments;
using FinalProject.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //TODO შევავსო კონტროლერის მხარეს გამოვიძახო სხვა ფუნქციოები სერვისებიდან

        [HttpGet]
        public async Task<IActionResult> AllComments()
        {
            var result = await _commentService.GetAllCommentsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var result = await _commentService.GetSingleCommentAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentForCreatingDTO comment)
        {
            await _commentService.AddCommentAsync(comment);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateComment(CommentForUpdatingDTO comment)
        {
            await _commentService.UpdateCommentAsync(comment);
            return Ok();
        }
    }
}
