using CSharpFunctionalExtensions;
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

        public async Task<Result<Project>> Get(int projectId)
        {
            var project = await _projectsRepository.Get(projectId);

            if (project == null)
            {
                return Result.Failure<Project>("The entered id does not exist");
            }

            return project;
        }

        public async Task<Project[]> Get()
        {
            return await _projectsRepository.Get();
        }

        public async Task<int> Create(Project project)
        {
            return await _projectsRepository.Add(project);
        }

        public async Task<int> Delete(int projectId)
        {
            return await _projectsRepository.Delete(projectId);
        }
    }
}