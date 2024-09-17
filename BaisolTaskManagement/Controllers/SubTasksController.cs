using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaisolTaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private readonly ISubTasks _subTaskRepository;
        private readonly IMapper _mapper;

        public SubTasksController(ISubTasks subTaskRepository, IMapper mapper)
        {
            _subTaskRepository = subTaskRepository;
            _mapper = mapper;
        }

        [HttpPost("Add-SubTask")]
        public async Task<IActionResult> AddSubTask([FromBody] SubTasksDto subTasksDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _subTaskRepository.AddSubTask(subTasksDto);
            if (result == "SubTask added successfully.")
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("Get-All-SubTask")]
        public async Task<IActionResult> GetAllSubTasks()
        {
            var subTasks = await _subTaskRepository.GetAllSubTasks();
            if (subTasks == null || subTasks.Count == 0)
            {
                return NotFound("No subTasks found");
            }
            return Ok(subTasks);
        }

        [HttpGet("Get-SubTask")]
        public async Task<IActionResult> GetSubTaskById(int subTaskId)
        {
            var subTaskDto = await _subTaskRepository.GetSubTaskById(subTaskId);
            if (subTaskDto == null)
            {
                return NotFound("SubTask not found");
            }
            return Ok(subTaskDto);
        }

        [HttpPut("Update-SubTask")]
        public async Task<IActionResult> UpdateSubTask([FromBody] SubTasksDto subTasksDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _subTaskRepository.UpdateSubTask(subTasksDto);
            if (isUpdated)
            {
                return Ok("SubTask updated successfully");
            }
            else
            {
                return NotFound("SubTask not found");
            }
        }

        [HttpDelete("Delete-SubTask")]
        public async Task<IActionResult> DeleteSubTask(int subTaskId)
        {
            var isDeleted = await _subTaskRepository.DeleteSubTask(subTaskId);
            if (isDeleted)
            {
                return Ok("SubTask deleted successfully");
            }
            else
            {
                return NotFound("SubTask not found");
            }
        }

    }
}
