namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesRepository
    {
        Task<Salary?> Get(int employeeId);

        Task<int> Save(Salary salary);
    }
}