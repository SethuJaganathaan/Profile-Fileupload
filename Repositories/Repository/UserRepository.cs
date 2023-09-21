using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.DTO.Common;
using Repositories.DTO.Users;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        string result = "action successfull";
        private readonly UsersContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(UsersContext dbcontext, IMapper mapper, ILogger<UserRepository> logger)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> AddUser(UserCreateDTO userCreateDTO)
        {
            _logger.LogInformation("AddUser method invoked");
            var userId = Guid.NewGuid();

            var user = _mapper.Map<User>(userCreateDTO);
            user.UserId = userId;

            using (var memoryStream = new MemoryStream())
            {
                await userCreateDTO.ProfilePicture.CopyToAsync(memoryStream);
                user.ProfilePicture = memoryStream.ToArray();
            }

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(Guid Id)
        {
            var user = await GetUserById(Id);
            _dbcontext.Users.Remove(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            _logger.LogInformation("UserById method invoked");

            return await _dbcontext.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            _logger.LogInformation("GetUser method Invoked");

            return await _dbcontext.Users.ToListAsync();
        }

        public async Task<string> UpdateUser(Guid userid, UserUpdateDTO user)
        {
            _logger.LogInformation("UpdateUser method invoked");

            var existingUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == userid);

            if (existingUser == null)
            {
                return "Not Found";
            }

            _mapper.Map(user, existingUser);

            await _dbcontext.SaveChangesAsync();

            return result;
        }

        public async Task<List<UserInformationDTO>> GetAllUserInformation()
        {
            var query = from info in _dbcontext.Information
                        join user in _dbcontext.Users on info.UserId equals user.UserId
                        select new UserInformationDTO
                        {
                            UserId = user.UserId,
                            Username = user.Username,
                            InformationId = info.InformationId,
                            Name = info.Name,
                            InfoFile = info.InfoFile,
                            Description = info.Description
                        };

            return await query.ToListAsync();
        }
    }
}