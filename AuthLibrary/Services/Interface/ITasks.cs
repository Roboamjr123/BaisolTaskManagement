using AuthLibrary.Dtos;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuthLibrary.Services.Interface
{
    public interface ITasks
    {
        Task<ICollection<TasksDto>> GetAllTasks();
        Task<TasksDto> GetTaskById(int taskId);
        Task<string> AddTask(TasksDto tasksDto);
        Task<bool> UpdateTask(TasksDto tasksDto);
        Task<bool> DeleteTask(int taskId);
        Task<bool> Save(); 
    }
}
