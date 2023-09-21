using Microsoft.AspNetCore.Mvc;
using Repositories.DTO.Users;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> AddUser([FromQuery] UserCreateDTO user)
        {
            if (user.ProfilePicture != null &&
                (user.ProfilePicture.ContentType != "image/jpeg" &&
                user.ProfilePicture.ContentType != "image/png") &&
                user.ProfilePicture.ContentType != "image/jpg")
            {
                return BadRequest("Only PNG and JPG files are allowed.");
            }

            var result = await _userService.AddUser(user);
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid userid, [FromQuery] UserUpdateDTO user)
        {
            var result = await _userService.UpdateUser(userid, user);
            return Ok(result);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok("User Deleted");
        }

        [HttpGet("UsersInformation")]
        public async Task<IActionResult> GetAllUserInformation()
        {
            var result = await _userService.GetAllUserInformation();
            return Ok(result);
        }
    }
}