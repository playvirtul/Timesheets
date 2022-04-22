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

        public async Task<int> AddWorkTime(WorkTime workTime)
        {
            var workTimeId = await _workTimesRepository.Add(workTime);

            return workTimeId;
        }
    }

    // public static class Projects
    // {
    //     private static List<Project> _projectList = new List<Project>();
    //
    //     public static int Add(Project project)
    //     {
    //         var id = _projectList.Count + 1;
    //
    //         _projectList.Add(project with { Id = id });
    //
    //         return id;
    //     }
    //
    //     public static Project[] Get()
    //     {
    //         return _projectList.ToArray();
    //     }
    //
    //     public static Project? Get(int projectId)
    //     {
    //         return _projectList.FirstOrDefault(x => x.Id == projectId);
    //     }
    //
    //     public static void Delete(int projectId)
    //     {
    //         var project = _projectList.FirstOrDefault(x => x.Id == projectId);
    //
    //         _projectList.Remove(project);
    //     }
    // }
}