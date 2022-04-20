namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task Add(Employee employee);

        Task<Employee[]> Get();
    }
}