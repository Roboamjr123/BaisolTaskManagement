using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using DataLibrary.Database;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Services.Repositories
{
    public class ProjectsRepository : IProjects
    {
        private readonly DataContext _dataContext;

        public ProjectsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddProject(ProjectsDto projectsDto)
        {
            // Convert DueDate to UTC if using DateTime
            var dueDateUtc = projectsDto.DueDate?.ToUniversalTime();

            // Map the DTO to the project entity
            var project = new Projects
            {
                Project_Id = projectsDto.Project_Id,
                Project_Name = projectsDto.Project_Name,
                Description = projectsDto.Description,
                DueDate = dueDateUtc
            };

            // Add the new project to the database
            await _dataContext.Projects.AddAsync(project);
            var result = await Save();
            return result ? "Project added successfully." : "Failed to add project.";
        }

        public async Task<ICollection<Projects>> GetAllProject()
        {
            return await _dataContext.Projects.ToListAsync();
        }

        public async Task<ProjectsDto> GetProjectById(int projectId)
        {
            var project = await _dataContext.Projects.FindAsync(projectId);
            if (project == null) return null;

            var projectDto = new ProjectsDto
            {
                Project_Id = project.Project_Id,
                Project_Name = project.Project_Name,
                Description = project.Description,
                DueDate = project.DueDate
            };
            return projectDto;
        }

        public async Task<bool> UpdateProject(ProjectsDto projectsDto)
        {
            var project = await _dataContext.Projects.FindAsync(projectsDto.Project_Id);
            if (project == null) return false;

            // Convert DueDate to UTC if using DateTime
            var dueDateUtc = projectsDto.DueDate?.ToUniversalTime();

            project.Project_Name = projectsDto.Project_Name;
            project.Description = projectsDto.Description;
            project.DueDate = dueDateUtc;

            _dataContext.Projects.Update(project);
            return await Save();
            
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            var project = await _dataContext.Projects.FindAsync(projectId);
            if (project == null) return false;

            _dataContext.Projects.Remove(project);
            return await Save();
        }

        public async Task<bool> Save()
        {
            // Save changes to the database
            var saved = _dataContext.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

       
    }
}
