using Microsoft.Extensions.Logging;
using Repositories.DTO.Common;
using Repositories.DTO.Users;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public Task<User> AddUser(UserCreateDTO user)
        {
            return _userRepository.AddUser(user);
        }

        public Task DeleteUser(Guid Id)
        {
            return _userRepository.DeleteUser(Id);
        }

        public async Task<List<UserInformationDTO>> GetAllUserInformation()
        {
            return await _userRepository.GetAllUserInformation();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }


        public async Task<string> UpdateUser(Guid userid, UserUpdateDTO user)
        {
            return await _userRepository.UpdateUser(userid, user);
        }
    }
}