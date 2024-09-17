using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaisolTaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjects _projectRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjects projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpPost("Add-Project")]
        public async Task<IActionResult> AddProject([FromBody] ProjectsDto projectsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _projectRepository.AddProject(projectsDto);
            return Ok(result);
        }

        [HttpGet("Get-All-Project")]
        public async Task<ActionResult<IEnumerable<ProjectsDto>>> GetAllProject()
        {
            var projects = await _projectRepository.GetAllProject();
            var projectsDto = _mapper.Map<IEnumerable<ProjectsDto>>(projects);
            return Ok(projectsDto);
        }

        [HttpGet("Get-Project")]
        public async Task<ActionResult<ProjectsDto>> GetProjectById(int projectId)
        {
            var project = await _projectRepository.GetProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }
            var projectDto = _mapper.Map<ProjectsDto>(project);
            return Ok(projectDto);
        }

        [HttpPut("Update-Project")]
        public async Task<ActionResult<bool>> UpdateProject(int id, [FromBody] ProjectsDto projectsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != projectsDto.Project_Id)
            {
                return BadRequest("Project Id does not match.");
            }
            var updated = await _projectRepository.UpdateProject(projectsDto);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete("Delete-Project")]
        public async Task<ActionResult<bool>> DeleteProject(int id)
        {
            var deleted = await _projectRepository.DeleteProject(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }


    }
}
