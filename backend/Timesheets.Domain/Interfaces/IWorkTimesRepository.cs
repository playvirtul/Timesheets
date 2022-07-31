namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesRepository
    {
        Task<int> Add(WorkTime newWorkTime);

        Task<WorkTime[]> Get(int projectId);

        Task<WorkTime[]> Get(int employeeId, int month, int year);
    }
}