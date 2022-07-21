using AutoMapper;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<User, Domain.User>().ReverseMap();

            CreateMap<Domain.Invitation, Invitation>();

            CreateMap<Invitation, Domain.TelegramInvitation>();

            CreateMap<Project, Domain.Project>().ReverseMap();

            CreateMap<Salary, Domain.Salary>().ReverseMap();

            CreateMap<WorkTime, Domain.WorkTime>().ReverseMap();

            CreateMap<Employee, Domain.Employee>().ReverseMap();

            CreateMap<Employee, Domain.Chief>();

            CreateMap<Employee, Domain.StaffEmployee>();

            CreateMap<Employee, Domain.Manager>();

            CreateMap<Employee, Domain.Freelancer>();
        }
    }
}