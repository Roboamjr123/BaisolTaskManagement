using AuthLibrary.Dtos;

namespace AuthLibrary.Services.Interface
{
     public interface ISubDetails
    {
        Task<ICollection<SubDetailsDto>> GetAllSubDetails();
        Task<SubDetailsDto> GetSubDetailById(int subdetailId);
        Task<string> AddSubDetail(SubDetailsDto subdetailsDto);
        Task<bool> UpdateSubDetail(SubDetailsDto subdetailsDto);
        Task<bool> DeleteSubDetail(int subdetailId);
        Task<bool> Save();
    }
}
