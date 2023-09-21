using Microsoft.AspNetCore.Http;

namespace Repositories.DTO.Users
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}