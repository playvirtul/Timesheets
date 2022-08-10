using CSharpFunctionalExtensions;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class SalariesService : ISalariesService
    {
        private readonly ISalariesRepository _salariesRepository;
        private readonly IWorkTimesRepository _workTimesRepository;

        public SalariesService(ISalariesRepository salariesRepository, IWorkTimesRepository workTimesRepository)
        {
            _salariesRepository = salariesRepository;
            _workTimesRepository = workTimesRepository;
        }

        public async Task<Result<Salary>> Get(int employeeId)
        {
            var salary = await _salariesRepository.Get(employeeId);

            if (salary == null)
            {
                return Result.Failure<Salary>("The entered id does not exist");
            }

            return salary;
        }

        public async Task<int> Save(Salary salary)
        {
            return await _salariesRepository.Save(salary);
        }

        public async Task<Result<Report>> SalaryCalculation(int employeeId, int month, int year)
        {
            if (month < 1 || month > 12)
            {
                return Result.Failure<Report>("The number of months must be between 1 and 12");
            }

            var salary = await _salariesRepository.Get(employeeId);

            if (salary == null)
            {
                return Result.Failure<Report>("An employee with this id has no salary");
            }

            var workTimesPerMonth = await _workTimesRepository.Get(employeeId, month, year);

            var hoursPerMonth = workTimesPerMonth.Sum(x => x.Hours);
            var salaryAmountPerMonth = salary.SalaryCalculation(workTimesPerMonth);

            return new Report
            {
                Hours = hoursPerMonth,
                SalaryAmount = salaryAmountPerMonth
            };
        }
    }
}