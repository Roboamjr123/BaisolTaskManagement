using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using AutoMapper;
using DataLibrary.Database;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthLibrary.Services.Repositories
{
    public class TasksRepository : ITasks
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TasksRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> AddTask(TasksDto tasksDto)
        {
            var project = await _dataContext.Projects
                                            .Where(p => p.Project_Id == tasksDto.Project_Id)
                                            .FirstOrDefaultAsync();

            if (project == null)
            {
                return "Project not found.";
            }

            // Use IMapper to map DTO to entity
            var task = _mapper.Map<Tasks>(tasksDto);

            // Convert dates to UTC
            task.PlannedStartDate = tasksDto.PlannedStartDate?.ToUniversalTime();
            task.PlannedEndDate = tasksDto.PlannedEndDate?.ToUniversalTime();
            task.ActualStartDate = tasksDto.ActualStartDate?.ToUniversalTime();
            task.ActualEndDate = tasksDto.ActualEndDate?.ToUniversalTime();

            await _dataContext.Tasks.AddAsync(task);
            return await Save() ? "Task added successfully." : "Failed to add task.";
        }

        public async Task<ICollection<TasksDto>> GetAllTasks()
        {
            var tasks = await _dataContext.Tasks
                                          .Include(t => t.Project)
                                          .ToListAsync();

            return tasks.Select(task => _mapper.Map<TasksDto>(task)).ToList();
        }

        public async Task<TasksDto> GetTaskById(int taskId)
        {
            var task = await _dataContext.Tasks
                                         .Where(t => t.Task_Id == taskId)
                                         .FirstOrDefaultAsync();

            return task == null ? null : _mapper.Map<TasksDto>(task);
        }

        public async Task<bool> UpdateTask(TasksDto tasksDto)
        {
            var task = await _dataContext.Tasks
                .Include(t => t.Project)
                .Where(t => t.Task_Id == tasksDto.Task_Id)
                .FirstOrDefaultAsync();

            if (task == null || task.Project_Id != tasksDto.Project_Id)
            {
                return false; // Task not found or Project_Id mismatch
            }

            // Update task properties
            task.Task_Name = tasksDto.Task_Name;
            task.PlannedStartDate = tasksDto.PlannedStartDate?.ToUniversalTime();
            task.PlannedEndDate = tasksDto.PlannedEndDate?.ToUniversalTime();
            task.ActualStartDate = tasksDto.ActualStartDate?.ToUniversalTime();
            task.ActualEndDate = tasksDto.ActualEndDate?.ToUniversalTime();
            task.duration = tasksDto.duration;
            task.progress = tasksDto.progress;

            _dataContext.Tasks.Update(task);
            return await Save();
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await _dataContext.Tasks
                                 .Include(t => t.Project)
                                 .Where(t => t.Task_Id == taskId)
                                 .FirstOrDefaultAsync();

            if (task == null)
            {
                return false; // Task not found
            }

            _dataContext.Tasks.Remove(task);
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
