namespace Timesheets.Domain.Interfaces
{
    public interface ISalariesService
    {
        Task SetupSalary(Employee employee);
    }
}