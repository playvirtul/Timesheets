namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesRepository
    {
        Task<int> Add(WorkTime newWorkTime);

        Task<WorkTime[]> Get();
    }
}