namespace Timesheets.Domain.Interfaces
{
    public interface IProjectsRepository
    {
        Task<int> Create(Project newProject);

        Task<Project[]> Get();
    }
}