using FinalProject.Contracts.Users;
using FinalProject.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> AllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetSingleUserAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserForUpdatingDTO user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok();
        }

        [HttpPut("block")]
        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> BlockUser(string id)
        {
            await _userService.BlockUserAsync(id);
            return Ok();
        }
        [HttpPut("unblock")]
        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public async Task<IActionResult> UnblockUser(string id)
        {
            await _userService.UnblockUserAsync(id);
            return Ok();
        }
    }
}
