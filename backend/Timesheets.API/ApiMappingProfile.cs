﻿using AutoMapper;
using Timesheets.API.Contracts;
using Timesheets.Domain;

namespace Timesheets.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Project, GetProjectResponse>();

            CreateMap<Employee, GetEmployeeResponse>();

            CreateMap<Salary, GetSalaryResponse>();

            CreateMap<Invitation, GetInvitationResponse>();
        }
    }
}