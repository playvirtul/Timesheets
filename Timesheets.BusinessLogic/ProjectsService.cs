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
            bool isValid = !string.IsNullOrWhiteSpace(newProject.EmployeeName) 
                && newProject.Id > default(int)
                && newProject.WorkingHours >= default(int)
                && !Projects.ProjectsList.Any(x => x.Id == newProject.Id);

            if (!isValid)
            {
                throw new ArgumentException();
            }

            Projects.ProjectsList.Add(newProject);

            return newProject.Id;
        }

        public async Task<int> Delete(int projectId)
        {
            if (projectId <= default(int))
            {
                throw new ArgumentException();
            }

            var project = Projects.ProjectsList.FirstOrDefault(x => x.Id == projectId);

            Projects.ProjectsList.Remove(project);
            return project.Id;
        }

        public async Task AddWorkingHours(string employeeName, int hours)
        {
            var index = Projects.ProjectsList.FindIndex(x => x.EmployeeName == employeeName);

            Projects.ProjectsList[index].WorkingHours += hours;
        }
    }

    public static class Projects
    {
        public static List<Project> ProjectsList { get; set; } = new List<Project>();
    }
}