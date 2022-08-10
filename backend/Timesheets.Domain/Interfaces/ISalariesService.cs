using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task<int> Save(Salary salary);

        Task<Result<Salary>> Get(int employeeId);

        Task<Result<Report>> SalaryCalculation(int employeeId, int month, int year);
    }
}