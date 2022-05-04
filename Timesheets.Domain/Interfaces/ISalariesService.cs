namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task SetupSalary(Salary salary);
    }
}