namespace Timesheets.Domain.Interfaces
{
    public interface IProjectsRepository
    {
        Task<int> Add(Project newProject);

        Task<Project[]> Get();

        Task<Project?> Get(int projectId);

        Task<int> Delete(int projectId);
    }
}