using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.DTO.Common;
using Repositories.DTO.Information;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repository
{
    public class InformationRepository : IInformationRepository
    {
        string result = "action successfull";
        private readonly UsersContext _dbContext;
        private readonly IMapper _mapper;
        public InformationRepository(UsersContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Information>> GetInformation()
        {
            return await _dbContext.Information.ToListAsync();
        }

        public async Task<Information> GetInformationById(Guid id)
        {
            return await _dbContext.Information.FindAsync(id);
        }

        public async Task<List<UserInformationDTO>> GetUserInformation(Guid userId)
        {
            var query = from info in _dbContext.Information
                        join user in _dbContext.Users on info.UserId equals user.UserId
                        where info.UserId == userId
                        select new UserInformationDTO
                        {
                            Username = user.Username,
                            UserId = user.UserId,
                            InformationId = info.InformationId,
                            Name = info.Name,
                            InfoFile = info.InfoFile,
                            Description = info.Description
                        };

            return await query.ToListAsync();
        }

        public async Task<string> UploadInformation(UploadInformationDTO request)
        {
            try
            {
                foreach (var file in request.Files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var bytes = new byte[file.Length];

                        using (var stream = file.OpenReadStream())
                        {
                            await stream.ReadAsync(bytes, 0, (int)file.Length);
                        }

                        var information = new Information
                        {
                            InformationId = Guid.NewGuid(),
                            UserId = request.UserId,
                            //Name = Path.GetFileName(fileName),
                            Name = request.Filename,
                            InfoFile = bytes,
                            ContentType = file.ContentType,
                            Description = request.Description
                        };

                        _dbContext.Information.Add(information);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating info", ex);
            }
        }

        public InformationCreateDTO GetById(Guid id)
        {
            var informationEntity = _dbContext.Information.FirstOrDefault(p => p.InformationId == id);

            if (informationEntity == null)
            {
                return null;
            }

            var informationDTO = _mapper.Map<InformationCreateDTO>(informationEntity);

            return informationDTO;
        }

        public async Task<string> DeleteInformation(Guid id)
        {
            var information = await _dbContext.Information.FindAsync(id);

            if (information == null)
            {
                return null; 
            }

            _dbContext.Information.Remove(information);
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<string> UpdateInformation(Guid Infoid, InformationUpdateDTO updateDTO)
        {
            var existingInformation = await _dbContext.Information.FirstOrDefaultAsync(p => p.InformationId == Infoid);

            if (existingInformation == null)
            {
                return "Not Found";
            }

            _mapper.Map(updateDTO, existingInformation);
            existingInformation.ContentType = updateDTO.InfoFile.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await updateDTO.InfoFile.CopyToAsync(memoryStream);
                existingInformation.InfoFile = memoryStream.ToArray();
            }

            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}