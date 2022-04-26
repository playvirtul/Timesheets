namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task<int> Create(Employee employee);

        Task<Employee[]> Get();

        Task<Employee?> Get(int employeeId);

        Task<bool> Delete(int employeeId);
    }
}