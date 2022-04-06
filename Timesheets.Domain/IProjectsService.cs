using System.Threading.Tasks;

namespace Timesheets.Domain
{
    public interface IProjectsService
    {
        Task<Project[]> Get();
        
        Task<int> Create(Project newProject);

        Task<int> Delete(int projectId);

        Task AddWorkingHours(string employeeName, int hours);
    }
}