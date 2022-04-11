namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task Add(Employee employee);

        Task<Employee[]> Get();
    }
}