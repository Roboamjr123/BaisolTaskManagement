using AuthLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Services.Interface
{
    public interface ISubTasks
    {
        Task<ICollection<SubTasksDto>> GetAllSubTasks();
        Task<SubTasksDto> GetSubTaskById(int subtaskId);
        Task<string> AddSubTask(SubTasksDto subtasksDto);
        Task<bool> UpdateSubTask(SubTasksDto subtasksDto);
        Task<bool> DeleteSubTask(int subtaskId);
        Task<bool> Save();
    }
}
