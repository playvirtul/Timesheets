namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesService
    {
        Task<string> Add(WorkTime workTime);

        Task<WorkTime[]> Get(int employeeId);
    }
}