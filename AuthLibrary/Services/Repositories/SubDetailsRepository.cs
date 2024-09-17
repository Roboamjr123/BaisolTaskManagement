using AuthLibrary.Dtos;
using AuthLibrary.Services.Interface;
using DataLibrary.Database;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary.Services.Repositories
{
    public class SubDetailsRepository : ISubDetails
    {
        private readonly DataContext _dataContext;

        public SubDetailsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddSubDetail(SubDetailsDto subdetailsDto)
        {
            // Ensure the associated SubTask exists
            var subtask = await _dataContext.SubTasks
                                            .Where(st => st.SubT_Id == subdetailsDto.SubT_Id)
                                            .FirstOrDefaultAsync();
            if (subtask == null)
            {
                return "Sub Task not found.";
            }

            var subdetail = new SubDetails
            {
                SubD_Name = subdetailsDto.SubD_Name,
                SubT_Id = subdetailsDto.SubT_Id,
                Description = subdetailsDto.Description,
                ImageUrl = subdetailsDto.ImageUrl
                
            };

            await _dataContext.SubDetails.AddAsync(subdetail);
            var result = await Save();
            return result ? "Sub Detail added successfully." : "Failed to add sub detail.";

        }


        public async Task<ICollection<SubDetailsDto>> GetAllSubDetails()
        {
            var subdetails = await _dataContext.SubDetails
                                               .Include(sd => sd.SubTask)
                                               .ToListAsync();

            var subDetailsDto = subdetails.Select(sd => new SubDetailsDto
            {
                SubD_Id = sd.SubD_Id,
                SubD_Name = sd.SubD_Name,
                SubT_Id = sd.SubT_Id,
                Description = sd.Description,
                ImageUrl = sd.ImageUrl
 
            }).ToList();

            return subDetailsDto;
        }

        public async Task<SubDetailsDto> GetSubDetailById(int subdetailId)
        {
            var subdetail = await _dataContext.SubDetails
                                              .Where(sd => sd.SubD_Id == subdetailId)
                                              .Include(sd => sd.SubTask)
                                              .FirstOrDefaultAsync();

            if (subdetail == null)
            {
                return null;
            }

            var subDetailsDto = new SubDetailsDto
            {
                SubD_Id = subdetail.SubD_Id,
                SubD_Name = subdetail.SubD_Name,
                SubT_Id = subdetail.SubT_Id,
                Description = subdetail.Description,
                ImageUrl = subdetail.ImageUrl
               
            };

            return subDetailsDto;
        }

        public async Task<bool> UpdateSubDetail(SubDetailsDto subdetailsDto)
        {
            var subdetail = await _dataContext.SubDetails
                                              .Where(sd => sd.SubD_Id == subdetailsDto.SubD_Id)
                                              .FirstOrDefaultAsync();

            if (subdetail == null)
            {
                return false;
            }

            subdetail.SubD_Name = subdetailsDto.SubD_Name;
            subdetail.SubT_Id = subdetailsDto.SubT_Id;
            subdetail.Description = subdetailsDto.Description;
            subdetail.ImageUrl = subdetailsDto.ImageUrl;
           

            _dataContext.SubDetails.Update(subdetail);
            return await Save();
        }

        public async Task<bool> DeleteSubDetail(int subdetailId)
        {
            var subdetail = await _dataContext.SubDetails
                                              .Where(sd => sd.SubD_Id == subdetailId)
                                              .FirstOrDefaultAsync();

            if (subdetail == null)
            {
                return false;
            }

            _dataContext.SubDetails.Remove(subdetail);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = _dataContext.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

    }
}
