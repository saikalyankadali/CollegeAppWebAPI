using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;

namespace CollegeApp.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Student, StudentDTO>().ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name)).ReverseMap();
        }
    }
}
