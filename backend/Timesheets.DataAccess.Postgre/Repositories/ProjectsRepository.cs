﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.Project?> Get(int projectId)
        {
            var projectEntity = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (projectEntity == null)
            {
                return null;
            }

            var project = _mapper.Map<Project, Domain.Project>(projectEntity);

            return project;
        }

        public async Task<Domain.Project[]> Get()
        {
            var projectEntities = await _context.Projects
                .AsNoTracking()
                .ToArrayAsync();

            var projects = _mapper.Map<Project[], Domain.Project[]>(projectEntities);

            return projects;
        }

        public async Task<int> Add(Domain.Project newProject)
        {
            var project = _mapper.Map<Domain.Project, Project>(newProject);

            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task<int> Delete(int projectId)
        {
            _context.Projects.Remove(new Project { Id = projectId });

            await _context.SaveChangesAsync();

            return projectId;
        }
    }
}