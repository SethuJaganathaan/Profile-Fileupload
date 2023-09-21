using Repositories.DTO.Common;
using Repositories.DTO.Users;
using Repositories.Entities;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();

        Task<User> AddUser(UserCreateDTO user);

        Task<User> GetUserById(Guid id);

        Task<string> UpdateUser(Guid userid, UserUpdateDTO user);

        Task DeleteUser(Guid Id);

        Task<List<UserInformationDTO>> GetAllUserInformation();
    }
}