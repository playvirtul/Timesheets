using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;

namespace Timesheets.BusinessLogic
{
    public class ProjectsService : IProjectsService
    {
        public async Task<Project?> Get(int projectId)
        {
            return Projects.Get(projectId);
        }

        public async Task<Project[]> Get()
        {
            return Projects.Get();
        }

        public async Task<int> Create(Project newProject)
        {
            return Projects.Add(newProject);
        }

        public async Task<bool> Delete(int projectId)
        {
            Projects.Delete(projectId);

            return true;
        }

        public async Task<string[]> AddWorkTime(WorkTime workTime)
        {
            var project = Projects.Get(workTime.ProjectId);

            if (project == null)
            {
                return new string[] { "Project is null" };
            }

            return project.AddWorkTime(workTime);
        }
    }

    public static class Projects
    {
        private static List<Project> ProjectsList { get; set; } = new List<Project>();

        public static int Add(Project project)
        {
            var id = ProjectsList.Count + 1;

            ProjectsList.Add(project with { Id = id });

            return id;
        }

        public static Project[] Get()
        {
            return ProjectsList.ToArray();
        }

        public static Project? Get(int projectId)
        {
            return ProjectsList.FirstOrDefault(x => x.Id == projectId);
        }

        public static void Delete(int projectId)
        {
            var project = ProjectsList.FirstOrDefault(x => x.Id == projectId);

            ProjectsList.Remove(project);
        }
    }
}