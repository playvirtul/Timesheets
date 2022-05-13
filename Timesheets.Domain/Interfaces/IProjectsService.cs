namespace Timesheets.Domain.Interfaces
{
    public interface IProjectsService
    {
        Task<Project[]> Get();

        Task<Project?> Get(int projectId);

        Task<int> Create(Project newProject);

        Task<bool> Delete(int projectId);

        Task<string> AddProjectToEmployee(int projectId, int employeeId);
    }
}