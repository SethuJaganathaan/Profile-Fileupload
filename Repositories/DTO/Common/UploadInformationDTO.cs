using Microsoft.AspNetCore.Http;

namespace Repositories.DTO.Common
{
    public class UploadInformationDTO
    {
        public Guid UserId { get; set; }
        public string Filename { get; set; }    
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}