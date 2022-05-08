using System;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class WorkTimesService : IWorkTimesService
    {
        private readonly IWorkTimesRepository _workTimesRepository;

        public WorkTimesService(IWorkTimesRepository workTimesRepository)
        {
            _workTimesRepository = workTimesRepository;
        }

        public async Task<WorkTime[]> Get(int employeeId)
        {
            var workTimes = await _workTimesRepository.Get(employeeId);

            return workTimes;
        }

        public async Task<string> Create(WorkTime workTime)
        {
            var workTimes = await _workTimesRepository.Get(workTime.EmployeeId);
            var errors = Project.CreateWorkTime(workTime, workTimes);

            if (string.IsNullOrEmpty(errors))
            {
                await _workTimesRepository.Add(workTime);
                return string.Empty;
            }

            return errors;
        }
    }
}