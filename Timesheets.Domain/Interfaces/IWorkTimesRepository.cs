namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesRepository
    {
        Task<int> Create(WorkTime newWorkTime);

        Task<WorkTime[]> Get();
    }
}