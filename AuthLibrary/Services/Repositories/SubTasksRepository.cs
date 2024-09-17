using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using DataLibrary.Database;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;


namespace AuthLibrary.Services.Repositories
{
    public class SubTasksRepository : ISubTasks
    {
        private readonly DataContext _dataContext;

        public SubTasksRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddSubTask(SubTasksDto subtasksDto)
        {
            var task = await _dataContext.Tasks
                                         .Where(st => st.Task_Id == subtasksDto.Task_Id)
                                         .FirstOrDefaultAsync();
            if (task == null)
            {
                return " Task not found.";
            }

            var PlannedStartedDateUtc = subtasksDto.PlannedStartDate?.ToUniversalTime();
            var PlannedEndDateUtc = subtasksDto.PlannedEndDate?.ToUniversalTime();
            var ActualStartedDateUtc = subtasksDto.ActualStartDate?.ToUniversalTime();
            var ActualEndDateUtc = subtasksDto.ActualEndDate?.ToUniversalTime();

            var subtask = new SubTasks
            {
                SubT_Name = subtasksDto.SubT_Name,
                Task_Id = subtasksDto.Task_Id,
                PlannedStartDate = PlannedStartedDateUtc,
                PlannedEndDate = PlannedEndDateUtc,
                ActualStartDate = ActualStartedDateUtc,
                ActualEndDate = ActualEndDateUtc
            };

            await _dataContext.SubTasks.AddAsync(subtask);
            var result = await Save();
            return result ? " Sub Task added Successfully." : "Failed to Add Sub Task";

        }

        public async Task<ICollection<SubTasksDto>> GetAllSubTasks()
        {
            var subtask = await _dataContext.SubTasks
                                            .Include(st => st.Task)
                                            .ToListAsync();

            var subtaskDto = subtask.Select(subtask => new SubTasksDto
            {
                SubT_Id = subtask.SubT_Id,
                SubT_Name = subtask.SubT_Name,
                Task_Id= subtask.Task_Id,
                PlannedStartDate = subtask.PlannedStartDate,
                PlannedEndDate = subtask.PlannedEndDate,
                ActualStartDate = subtask.ActualStartDate,
                ActualEndDate = subtask.ActualEndDate
            }).ToList();

            return subtaskDto;
        }

        public async Task<SubTasksDto> GetSubTaskById(int subtaskId)
        {
           var subtask = await _dataContext.SubTasks
                                           .Where(st => st.SubT_Id == subtaskId)
                                           .FirstOrDefaultAsync();
            if (subtask == null)
            {
                return null;
            }

            var subtaskDto = new SubTasksDto
            {
                SubT_Id = subtask.SubT_Id,
                Task_Id = subtask.Task_Id,
                SubT_Name = subtask.SubT_Name,
                PlannedStartDate = subtask.PlannedStartDate,
                PlannedEndDate = subtask.PlannedEndDate,
                ActualStartDate = subtask.ActualStartDate,
                ActualEndDate = subtask.ActualEndDate
            };

            return subtaskDto;
        }

        public async Task<bool> UpdateSubTask(SubTasksDto subtasksDto)
        {
            var subtask = await _dataContext.SubTasks
                                             .FirstOrDefaultAsync(st => st.SubT_Id == subtasksDto.SubT_Id);

            if (subtask == null)
            {
                return false;
            }

            subtask.SubT_Name = subtasksDto.SubT_Name;
            subtask.PlannedStartDate = subtasksDto.PlannedStartDate?.ToUniversalTime();
            subtask.PlannedEndDate = subtasksDto.PlannedEndDate?.ToUniversalTime();
            subtask.ActualStartDate = subtasksDto.ActualStartDate?.ToUniversalTime();
            subtask.ActualEndDate = subtasksDto.ActualEndDate?.ToUniversalTime();

            _dataContext.SubTasks.Update(subtask);
            return await Save();
        }

        public async Task<bool> DeleteSubTask(int subtaskId)
        {
            var subtask = await _dataContext.SubTasks
                                            .FirstOrDefaultAsync(st => st.SubT_Id == subtaskId);
            if (subtask == null)
            {
                return false;
            }

            _dataContext.SubTasks.Remove(subtask);
            return await Save();
        }

      

        public async Task<bool> Save()
        {
            var saved = _dataContext.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
