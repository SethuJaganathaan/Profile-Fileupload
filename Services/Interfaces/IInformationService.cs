using Repositories.DTO.Common;
using Repositories.DTO.Information;
using Repositories.Entities;

namespace Services.Interfaces
{
    public interface IInformationService
    {
        Task<List<Information>> GetInformation();

        Task<Information> GetInformationById(Guid id);

        Task<List<UserInformationDTO>> GetUserInformation(Guid userid);

        Task<string> UploadInformation(UploadInformationDTO request);

        InformationCreateDTO GetById(Guid id);

        Task<string> DeleteInformation(Guid id);

        Task<string> UpdateInformation(Guid Infoid, InformationUpdateDTO updateDTO);
    }
}