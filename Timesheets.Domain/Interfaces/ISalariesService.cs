namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task<int> Upsert(Salary salary);

        Task<Salary?> Get(int employeeId);

        Task<decimal> CalculateSalaryForTimePeriod(int employeeId, int month);
    }
}