using System.Threading.Tasks;

namespace Timesheets.Domain
{
    public interface IProjectsService
    {
        Task<Project[]> Get();
        
        Task<bool> Create(Project newProject);

        Task<bool> Delete(int projectId);

        Task<bool> AddWorkingHours(string employeeName, int hours);
    }
}