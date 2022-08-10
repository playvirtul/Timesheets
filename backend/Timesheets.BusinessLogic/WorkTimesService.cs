using CSharpFunctionalExtensions;
using System;
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

        public async Task<Result<int>> Add(WorkTime workTime)
        {
            var employee = await _employeesRepository.Get(workTime.EmployeeId);

            if (employee == null)
            {
                return Result.Failure<int>("Employee not found with this id.");
            }

            var errors = employee.AddWorkTime(workTime.ProjectId, workTime);

            if (!string.IsNullOrEmpty(errors))
            {
                return Result.Failure<int>(errors);
            }

            var employeeId = await _workTimesRepository.Add(workTime);

            return employeeId;
        }
    }
}