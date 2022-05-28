using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class WorkTimesService : IWorkTimesService
    {
        private readonly IWorkTimesRepository _workTimesRepository;
        private readonly IEmployeesRepository _employeesRepository;

        public WorkTimesService(IWorkTimesRepository workTimesRepository, IEmployeesRepository employeesRepository)
        {
            _workTimesRepository = workTimesRepository;
            _employeesRepository = employeesRepository;
        }

        public async Task<WorkTime[]> Get(int employeeId)
        {
            var workTimes = await _workTimesRepository.Get(employeeId);

            return workTimes;
        }

        public async Task<string> Add(WorkTime workTime)
        {
            var employee = await _employeesRepository.Get(workTime.EmployeeId);

            if (employee == null)
            {
                return new string("Employee not found with this id.");
            }

            var errors = employee.AddWorkTime(workTime.ProjectId, workTime);

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            await _workTimesRepository.Add(workTime);
            return string.Empty;
        }
    }
}