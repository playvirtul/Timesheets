using AutoMapper;
using Timesheets.API.Contracts;
using Timesheets.Domain;

namespace Timesheets.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Project, ProjectResponse>();

            CreateMap<Employee, EmployeeResponse>();

            CreateMap<Salary, SalaryResponse>();
        }
    }
}