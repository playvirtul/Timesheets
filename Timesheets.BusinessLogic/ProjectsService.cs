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

        public async Task<bool> Create(Project newProject)
        {
            bool isValid = !string.IsNullOrWhiteSpace(newProject.EmployeeName) 
                && newProject.Id > default(int)
                && !Projects.ProjectsList.Any(x => x.Id == newProject.Id);

            if (!isValid)
            {
                throw new ArgumentException();
            }

            Projects.ProjectsList.Add(newProject);

            return true;
        }

        public async Task<bool> Delete(int projectId)
        {
            if (projectId <= default(int))
            {
                throw new ArgumentException();
            }

            var project = Projects.ProjectsList.FirstOrDefault(x => x.Id == projectId);

            Projects.ProjectsList.Remove(project);
            return true;
        }

        public async Task<bool> AddWorkingHours(string employeeName, int hours)
        {
            if (hours <= default(int) || string.IsNullOrWhiteSpace(employeeName))
            {
                throw new ArgumentException();
            }

            var index = Projects.ProjectsList.FindIndex(x => x.EmployeeName == employeeName);

            Projects.ProjectsList[index].WorkingHours += hours;

            return true;
        }
    }

    public static class Projects
    {
        public static List<Project> ProjectsList { get; set; } = new List<Project>();
    }
}