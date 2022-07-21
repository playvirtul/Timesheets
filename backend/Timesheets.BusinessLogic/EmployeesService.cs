using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;
using Timesheets.Domain.Telegram;

namespace Timesheets.BusinessLogic
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ITelegramApiClient _telegramApiClient;

        public EmployeesService(IEmployeesRepository employeesRepository, ITelegramApiClient telegramApiClient)
        {
            _employeesRepository = employeesRepository;
            _telegramApiClient = telegramApiClient;
        }

        public async Task Create(Employee employee)
        {
            await _employeesRepository.Add(employee);
        }

        public async Task<bool> SendTelegramInvite(TelegramInvitation invitation)
        {
            return await _telegramApiClient.SendTelegramInvite(invitation);
        }

        public async Task<Employee[]> Get()
        {
            return await _employeesRepository.Get();
        }

        public async Task<Employee?> Get(int employeeId)
        {
            return await _employeesRepository.Get(employeeId);
        }

        public async Task<string> BindProject(int employeeId, int projectId)
        {
            var employee = await _employeesRepository.Get(employeeId);

            if (employee == null)
            {
                return new string("Employee not found with this id.");
            }

            var error = employee.AddProject(projectId);

            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }

            return await _employeesRepository.AddProjectToEmployee(employeeId, projectId);
        }

        public Task<bool> Delete(int employeeId)
        {
            return _employeesRepository.Delete(employeeId);
        }
    }
}