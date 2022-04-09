namespace Timesheets.Domain
{
    public interface IProjectsService
    {
        Task<Project[]> Get();
        
        Task<int> Create(Project newProject);

        Task<bool> Delete(int projectId);

        Task<bool> AddWorkingHours(int projectId, int hours);
    }
}