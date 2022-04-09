using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;

namespace Timesheets.BusinessLogic
{
    public class ProjectsService : IProjectsService
    {
        public async Task<Project[]> Get()
        {
            return new Project[0];
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

        public async Task<bool> AddWorkingHours(int projectId, int hours)
        {
            var project = Projects.Get(projectId);

            if (project == null)
            {
                return false;
            }

            project.AddHours(hours);

            return true;
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