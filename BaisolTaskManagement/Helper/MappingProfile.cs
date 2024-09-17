using AuthLibrary.Dtos;
using AutoMapper;
using DataLibrary.Models;

namespace BaisolTaskManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add your AutoMapper configuration here for the Web project.
            CreateMap<Projects, ProjectsDto>().ReverseMap();
            CreateMap<Tasks, TasksDto>().ReverseMap();
            CreateMap<SubTasks, SubTasksDto>().ReverseMap();
            CreateMap<SubDetails, SubDetailsDto>().ReverseMap();
        }
    }
}
