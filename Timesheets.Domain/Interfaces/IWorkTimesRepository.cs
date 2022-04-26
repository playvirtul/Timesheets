namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesRepository
    {
        Task<bool> Add(WorkTime newWorkTime);

        Task<WorkTime[]> Get();

        Task<WorkTime[]> Get(int projectId);
    }
}