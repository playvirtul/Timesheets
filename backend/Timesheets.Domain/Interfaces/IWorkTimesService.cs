using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IWorkTimesService
    {
        Task<Result<int>> Add(WorkTime workTime);

        Task<WorkTime[]> Get(int employeeId);
    }
}