using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
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

        public async Task<string> AddProjectToEmployee(int projectId, int employeeId)
        {
            return await _projectsRepository.AddProjectToEmployee(projectId, employeeId);
        }

        public async Task<bool> Delete(int projectId)
        {
            return await _projectsRepository.Delete(projectId);
        }
    }
}