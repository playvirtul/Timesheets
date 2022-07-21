namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task<bool> SendTelegramInvite(TelegramInvitation invitation);

        Task Create(Employee employee);

        Task<Employee[]> Get();

        Task<Employee?> Get(int employeeId);

        Task<string> BindProject(int employeeId, int projectId);

        Task<bool> Delete(int employeeId);
    }
}