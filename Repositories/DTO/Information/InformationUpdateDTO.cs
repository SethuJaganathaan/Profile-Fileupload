using Microsoft.AspNetCore.Http;

namespace Repositories.DTO.Information
{
    public class InformationUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile InfoFile { get; set; }
    }
}