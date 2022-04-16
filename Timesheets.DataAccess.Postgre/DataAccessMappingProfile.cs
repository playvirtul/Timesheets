using AutoMapper;

namespace Timesheets.DataAccess.Postgre
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Project, Domain.Project>();
            CreateMap<WorkTime, Domain.WorkTime>();
        }
    }
}