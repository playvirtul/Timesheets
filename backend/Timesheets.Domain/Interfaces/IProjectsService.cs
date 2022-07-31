using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IProjectsService
    {
        Task<Project[]> Get();

        Task<Result<Project>> Get(int projectId);

        Task<int> Create(Project newProject);

        Task<int> Delete(int projectId);
    }
}