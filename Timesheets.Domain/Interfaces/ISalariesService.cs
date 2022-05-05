namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task Upsert(Salary salary);

        Task<Salary> Get(int employeeId);
    }
}