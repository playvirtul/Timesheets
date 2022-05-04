namespace Timesheets.Domain.Interfaces
{
    public interface IProjectsRepository
    {
        Task<int> Add(Project newProject);

        Task<Project[]> Get();

        Task<Project?> Get(int projectId);

        Task<bool> Delete(int projectId);

        Task AddEmployeeToProject(int projectId, int employeeId);
    }
}