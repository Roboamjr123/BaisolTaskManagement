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
    public class TasksRepository : ITasks
    {
        private readonly DataContext _dataContext;
        public TasksRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddTask(TasksDto tasksDto)
        {
            var project = await _dataContext.Projects 
                                            .Where(p => p.Project_Id == tasksDto.Project_Id)
                                            .FirstOrDefaultAsync();
            if (project == null)
            {
                return " Project Task not found.";
            }

            var PlannedStartDateUtc = tasksDto.PlannedStartDate?.ToUniversalTime();
            var PlandedEndDateUtc = tasksDto.PlannedEndDate?.ToUniversalTime();
            var ActualStartDateUtc = tasksDto.ActualStartDate?.ToUniversalTime();
            var ActualEndDateUtc = tasksDto.ActualEndDate?.ToUniversalTime();

            var task = new Tasks
            {
                Task_Name = tasksDto.Task_Name,
                Project_Id = tasksDto.Project_Id,
                PlannedStartDate = PlannedStartDateUtc,
                PlannedEndDate = PlandedEndDateUtc,
                ActualStartDate = ActualStartDateUtc,
                ActualEndDate = ActualEndDateUtc,
                duration = tasksDto.duration,
                progress = tasksDto.progress
               
            };

            await _dataContext.Tasks.AddAsync(task);
            var result = await Save();
            return result ? "Task added successfully." : "Failed to add task.";
        }

        public async Task<ICollection<TasksDto>> GetAllTasks()
        {
            var tasks = await _dataContext.Tasks
                                          .Include(t => t.Project)
                                          .ToListAsync();

            var taskDto = tasks.Select(task => new TasksDto
            {
                Task_Id = task.Task_Id,
                Task_Name = task.Task_Name,
                Project_Id = task.Project_Id,
                PlannedStartDate = task.PlannedStartDate,
                PlannedEndDate = task.PlannedEndDate,
                ActualStartDate = task.ActualStartDate,
                ActualEndDate = task.ActualEndDate,
                duration = task.duration,
                progress = task.progress
                
            }).ToList();

            return taskDto;
        }

        public async Task<TasksDto> GetTaskById(int taskId)
        {
            var task = await _dataContext.Tasks
                                         .Where(t => t.Task_Id == taskId)
                                         .FirstOrDefaultAsync();
            if (task == null)
            {
                return null;
            }

            var taskDto = new TasksDto
            {
                Task_Id = task.Task_Id,
                Project_Id = task.Project_Id,
                Task_Name = task.Task_Name,
                PlannedStartDate = task.PlannedStartDate,
                PlannedEndDate = task.PlannedEndDate,
                ActualStartDate = task.ActualStartDate,
                ActualEndDate = task.ActualEndDate,
                duration = task.duration,
                progress = task.progress
            };

            return taskDto;
        }

        public async Task<bool> UpdateTask(TasksDto tasksDto)
        {
            // Retrieve the task including the related project
            var task = await _dataContext.Tasks
                .Include(t => t.Project) // Include the Project entity
                .Where(t => t.Task_Id == tasksDto.Task_Id)
                .FirstOrDefaultAsync();

            // Check if the task was found
            if (task == null)
            {
                return false; // Task not found
            }

            // Check if the provided Project_Id matches the task's Project_Id
            if (task.Project_Id != tasksDto.Project_Id)
            {
                return false; // The provided Project_Id does not match the task's Project_Id
            }

            // Convert dates to UTC
            var PlannedStartDateUtc = tasksDto.PlannedStartDate?.ToUniversalTime();
            var PlannedEndDateUtc = tasksDto.PlannedEndDate?.ToUniversalTime();
            var ActualStartDateUtc = tasksDto.ActualStartDate?.ToUniversalTime();
            var ActualEndDateUtc = tasksDto.ActualEndDate?.ToUniversalTime();

            // Update task properties
            task.Task_Name = tasksDto.Task_Name;
            task.Project_Id = tasksDto.Project_Id;
            task.PlannedStartDate = PlannedStartDateUtc;
            task.PlannedEndDate = PlannedEndDateUtc;
            task.ActualStartDate = ActualStartDateUtc;
            task.ActualEndDate = ActualEndDateUtc;
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
