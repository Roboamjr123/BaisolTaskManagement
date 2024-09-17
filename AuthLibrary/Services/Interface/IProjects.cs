using AuthLibrary.Dtos;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Services.Interface
{
    public interface IProjects
    {
        Task<ICollection<Projects>> GetAllProject();
        Task<ProjectsDto> GetProjectById(int projectId);
        Task<string> AddProject(ProjectsDto projectsDto);
        Task<bool> UpdateProject(ProjectsDto projectsDto);
        Task<bool> DeleteProject(int projectId);
        Task<bool>Save();


    }
}
