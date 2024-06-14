using FinalProject.Contracts.Posts;
using FinalProject.Models.Post;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostServices _postService;
        public PostsController(IPostServices postService)
        {
            _postService = postService;
        }

        //TODO შევავსო კონტროლერის მხარეს გამოვიძახო სხვა ფუნქციოები სერვისებიდან

        [HttpGet]
        public async Task<IActionResult> AllPosts()
        {
            var result = await _postService.GetAllPostsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var result = await _postService.GetSinglePostsAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(PostForCreatingDTO post)
        {
            await _postService.AddPostsAsync(post);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePost(PostForUpdatingDTO post)
        {
            await _postService.UpdatePostsAsync(post);
            return Ok();
        }

        [HttpPut("change-status")]
        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> ChangeStatus(PostForChangingStatusDTO postStatus)
        {
            await _postService.ChangePostsStatusAsync(postStatus);
            return Ok();
        }

        [HttpPut("change-state")]
        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> ChangeState(PostForChangingStateDTO postState)
        {
            await _postService.ChangePostsStateAsync(postState);
            return Ok();
        }


    }
}
