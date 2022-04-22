namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<int> Add(Employee newEmployee);

        Task<bool> Delete(int employeeId);

        Task<Employee[]> Get();

        Task<Employee?> Get(int employeeId);
    }
}