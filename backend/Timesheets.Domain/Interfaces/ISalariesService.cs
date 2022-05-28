namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task<int> Save(Salary salary);

        Task<Salary?> Get(int employeeId);

        Task<decimal> SalaryCalculation(int employeeId, int month, int year);
    }
}