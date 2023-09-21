using Repositories.DTO.Common;
using Repositories.DTO.Information;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Service
{
    public class InformationService : IInformationService
    {
       private readonly IInformationRepository _informationRepository;
       public InformationService(IInformationRepository informationRepository)
       {
           _informationRepository = informationRepository;
       }
       public async Task<List<Information>> GetInformation()
       {
           return await _informationRepository.GetInformation();
       }

       public async Task<Information> GetInformationById(Guid id)
       {
           return await _informationRepository.GetInformationById(id);
       }

       public InformationCreateDTO GetById(Guid id)
       {
           return _informationRepository.GetById(id);
       }

       public async Task<List<UserInformationDTO>> GetUserInformation(Guid userid)
       {
           return await _informationRepository.GetUserInformation(userid);
       }

       public async Task<string> UploadInformation(UploadInformationDTO request)
       {
           return await _informationRepository.UploadInformation(request);
       }

       public async Task<string> DeleteInformation(Guid id)
       {
           return await _informationRepository.DeleteInformation(id);
       }

       public async Task<string> UpdateInformation(Guid Infoid, InformationUpdateDTO updateDTO)
       {
           return await _informationRepository.UpdateInformation(Infoid, updateDTO);
       }
    }
}