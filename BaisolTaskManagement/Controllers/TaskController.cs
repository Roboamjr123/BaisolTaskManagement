using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaisolTaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasks _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(ITasks taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpPost("Add-Task")]
        public async Task<IActionResult> AddTask([FromBody] TasksDto tasksDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskRepository.AddTask(tasksDto);
            if (result == "Task added successfully.")
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
            
        }

        [HttpGet("Get-All-Task")]
        public async Task<IActionResult> GetAllTasks() 
        {
          var tasks = await _taskRepository.GetAllTasks();
          if(tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found");
            }
            return Ok(tasks);
        }

        [HttpGet("Get-Task")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var taskDto = await _taskRepository.GetTaskById(taskId);
            if(taskDto == null)
            {
                return NotFound("Task not found");
            }
            return Ok(taskDto);
        }

        [HttpPut("Update-Task")]
        public async Task<IActionResult> UpdateTask([FromBody] TasksDto tasksDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _taskRepository.UpdateTask(tasksDto);
            if (isUpdated)
            {
                return Ok("Task updated successfully");
            }
            else
            {
                return NotFound("Task not found");
            }
        }

        [HttpDelete("Delete-Task")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var isDeleted = await _taskRepository.DeleteTask(taskId);
            if (isDeleted)
            {
                return Ok("Task deleted successfully");
            }
            else
            {
                return NotFound("Task not found");
            }
            
        }
    }
}
