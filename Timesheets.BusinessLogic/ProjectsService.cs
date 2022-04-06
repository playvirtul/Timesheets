using Timesheets.Domain;

namespace Timesheets.BusinessLogic
{
    public class ProjectsService : IProjectsService
    {
        public async Task<Project[]> Get()
        {
            return new Project[0];
        }
    }
}