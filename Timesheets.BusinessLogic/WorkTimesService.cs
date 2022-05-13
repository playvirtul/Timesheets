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
                return new string("Employee is null");
            }

            var errors = employee.AddWorkTime(workTime.ProjectId, workTime);

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            await _workTimesRepository.Add(workTime);
            return string.Empty;
        }

        //public async Task<string> Add(WorkTime workTime)
        //{
        //    var workTimes = await _workTimesRepository.Get(workTime.EmployeeId);
        //    var errors = Project.CreateWorkTime(workTime, workTimes);

        //    if (string.IsNullOrEmpty(errors))
        //    {
        //        await _workTimesRepository.Add(workTime);
        //        return string.Empty;
        //    }

        //    return errors;
        //}
    }
}