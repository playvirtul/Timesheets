namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee[]> Get();

        Task<Employee?> Get(int employeeId);

        Task Add(Employee newEmployee);

        Task<string> AddProjectToEmployee(int employeeId, int projectId);

        Task<bool> Delete(int employeeId);
    }
}