using AutoMapper;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Project, Domain.Project>().ReverseMap();
            CreateMap<WorkTime, Domain.WorkTime>().ReverseMap();
        }
    }
}