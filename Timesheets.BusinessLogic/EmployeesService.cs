using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<int> Create(Employee employee)
        {
            return await _employeesRepository.Add(employee);
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

            var errors = employee.AddProject(projectId);

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            return await _employeesRepository.AddProjectToEmployee(employeeId, projectId);
        }

        public Task<bool> Delete(int employeeId)
        {
            return _employeesRepository.Delete(employeeId);
        }
    }
}