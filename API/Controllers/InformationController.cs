using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTO.Common;
using Repositories.DTO.Information;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("[controller]")]
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly IMapper _mapper;
        public InformationController(IInformationService informationService, IMapper mapper)
        {
            _informationService = informationService;
            _mapper = mapper;
        }

        [HttpGet("Informations")]
        public async Task<IActionResult> GetInformation()
        {
            var result = await _informationService.GetInformation();
            return Ok(result);
        }

        [HttpGet("GetInformationById")]
        public async Task<IActionResult> GetInformationById(Guid id)
        {
            var result = await _informationService.GetInformationById(id);
            return Ok(result);
        }

        [HttpGet("UserInformations")]
        public async Task<IActionResult> GetUserInformation(Guid userId)
        {
            var result = await _informationService.GetUserInformation(userId);
            return Ok(result);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadInformation([FromQuery] UploadInformationDTO request)
        {
            var result = await _informationService.UploadInformation(request);
            return Ok(result);
        }

        [HttpGet("Download")]
        public FileResult GetById(Guid id)
        {
            InformationCreateDTO file = _informationService.GetById(id);
            return File(file.InfoFile, file.ContentType, file.Name);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteInformation(Guid id)
        {
            var result = await _informationService.DeleteInformation(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateInformation(Guid Infoid, [FromQuery] InformationUpdateDTO request)
        {
            var result = await _informationService.UpdateInformation(Infoid, request);
            return Ok(result);
        }
    }
}