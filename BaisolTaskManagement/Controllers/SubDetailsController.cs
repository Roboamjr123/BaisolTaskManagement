using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaisolTaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubDetailsController : ControllerBase
    {
        private readonly ISubDetails _subDetailsRepository;
        private readonly IMapper _mapper;

        public SubDetailsController(ISubDetails subDetailsRepository, IMapper mapper)
        {
            _subDetailsRepository = subDetailsRepository;
            _mapper = mapper;
        }

        [HttpPost("Add-SubDetails")]
        public async Task<IActionResult> AddSubDetail([FromBody] SubDetailsDto subDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _subDetailsRepository.AddSubDetail(subDetailsDto);
            if (result == "SubDetails added successfully.")
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("Get-All-SubDetails")]
        public async Task<IActionResult> GetAllSubDetails()
        {
            var subDetails = await _subDetailsRepository.GetAllSubDetails();
            if (subDetails == null || subDetails.Count == 0)
            {
                return NotFound("No subDetails found");
            }
            return Ok(subDetails);
        }

        [HttpGet("Get-SubDetails-By-Id")]
        public async Task<IActionResult> GetSubDetailsById(int subDetailsId)
        {
            var subDetailsDto = await _subDetailsRepository.GetSubDetailById(subDetailsId);
            if (subDetailsDto == null)
            {
                return NotFound("SubDetails not found");
            }
            return Ok(subDetailsDto);
        }

        [HttpPut("Update-SubDetails")]
        public async Task<IActionResult> UpdateSubDetails([FromBody] SubDetailsDto subDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _subDetailsRepository.UpdateSubDetail(subDetailsDto);
            if (isUpdated)
            {
                return Ok("SubDetails updated successfully");
            }
            else
            {
                return NotFound("SubDetails not found");
            }
        }

        [HttpDelete("Delete-SubDetails")]
        public async Task<IActionResult> DeleteSubDetails(int subDetailsId)
        {
            var isDeleted = await _subDetailsRepository.DeleteSubDetail(subDetailsId);
            if (isDeleted)
            {
                return Ok("SubDetails deleted successfully");
            }
            else
            {
                return NotFound("SubDetails not found");
            }
        }


    }
}
