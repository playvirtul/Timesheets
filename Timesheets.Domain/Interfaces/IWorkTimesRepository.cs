namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesRepository
    {
        Task<bool> Add(WorkTime newWorkTime);

        Task<WorkTime[]> Get(int projectId);

        Task<Domain.WorkTime[]> Get(int employeeId, int month);
    }
}