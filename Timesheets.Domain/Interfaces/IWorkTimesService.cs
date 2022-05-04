namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesService
    {
        Task<string[]> Create(WorkTime workTime);

        Task<WorkTime[]> Get(int employeeId);
    }
}