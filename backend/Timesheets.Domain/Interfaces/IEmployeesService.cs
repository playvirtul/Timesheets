using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task<bool> SendTelegramInvite(TelegramInvitation invitation);

        Task<int> Create(Employee employee);

        Task<Employee[]> Get();

        Task<Result<Employee>> Get(int employeeId);

        Task<string> BindProject(int employeeId, int projectId);

        Task<bool> Delete(int employeeId);
    }
}