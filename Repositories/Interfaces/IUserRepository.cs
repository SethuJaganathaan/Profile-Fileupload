using Repositories.DTO.Common;
using Repositories.DTO.Users;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        Task<User> AddUser(UserCreateDTO user);

        Task<User> GetUserById(Guid id);

        Task<string> UpdateUser(Guid userid, UserUpdateDTO user);

        Task DeleteUser(Guid Id);

        Task<List<UserInformationDTO>> GetAllUserInformation();
    }
}