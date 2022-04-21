namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task Create(Employee employee);

        Task<Employee[]> Get();

        Task<Employee?> Get(int employeeId);

        Task<bool> Delete(int employeeId);
    }
}