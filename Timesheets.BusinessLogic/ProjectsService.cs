using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IWorkTimesRepository _workTimesRepository;

        public ProjectsService(IProjectsRepository projectsRepository, IWorkTimesRepository workTimesRepository)
        {
            _projectsRepository = projectsRepository;
            _workTimesRepository = workTimesRepository;
        }

        public async Task<Project?> Get(int projectId)
        {
            return await _projectsRepository.Get(projectId);
        }

        public async Task<Project[]> Get()
        {
            return await _projectsRepository.Get();
        }

        public async Task<int> Create(Project newProject)
        {
            return await _projectsRepository.Add(newProject);
        }

        public async Task<bool> Delete(int projectId)
        {
            return await _projectsRepository.Delete(projectId);
        }

        public async Task<bool> AddWorkTime(WorkTime workTime)
        {
            var project = await _projectsRepository.Get(workTime.ProjectId);

            if (project == null || project.AddWorkTime(workTime).Length != 0)
            {
                return false;
            }

            return await _workTimesRepository.Add(workTime);
        }
    }
}