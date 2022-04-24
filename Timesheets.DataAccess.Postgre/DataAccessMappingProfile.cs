using AutoMapper;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Project, Domain.Project>();
                //.ForMember(dest => dest.WorkTimes, opt => opt.MapFrom(x => x.WorkTimes));

            CreateMap<Domain.Project, Project>();

            CreateMap<WorkTime, Domain.WorkTime>().ReverseMap();

            CreateMap<Employee, Domain.Employee>().ReverseMap();

            CreateMap<Employee, Domain.Chief>();

            CreateMap<Employee, Domain.StuffEmployee>();

            CreateMap<Employee, Domain.Manager>();

            CreateMap<Employee, Domain.Freelancer>();
        }
    }
}