using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            // ошибка связанная с include
            var projectEntity = await _context.Projects
                .Include(p => p.WorkTimes)
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
                .Include(p => p.WorkTimes)
                .AsNoTracking()
                .ToArrayAsync();

            var projects = _mapper.Map<Project[], Domain.Project[]>(projectEntities);

            return projects;

            //var p = projectEntities.Select(x =>
            //{
            //    var o = _mapper.Map<List<WorkTime>, Domain.WorkTime[]>(x.WorkTimes);
            //    return _mapper.Map<Project, Domain.Project>(x);
            //}).ToArray();

            //return p;
        }

        public async Task<int> Add(Domain.Project newProject)
        {
            var project = _mapper.Map<Domain.Project, Project>(newProject);

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task<bool> Delete(int projectId)
        {
            _context.Projects.Remove(new Project { Id = projectId });

            await _context.SaveChangesAsync();

            return true;
        }
    }
}