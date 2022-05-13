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

        public async Task<Salary?> Get(int employeeId)
        {
            return await _salariesRepository.Get(employeeId);
        }

        public async Task<int> Upsert(Salary salary)
        {
            return await _salariesRepository.Upsert(salary);
        }

        public async Task<decimal> CalculateSalaryForTimePeriod(int employeeId, int month)
        {
            if (month < 1 || month > 12)
            {
                return default(decimal);
            }

            var salary = await _salariesRepository.Get(employeeId);

            if (salary == null)
            {
                return default;
            }

            var workTimesPerMonth = await _workTimesRepository.Get(employeeId, month);

            return salary.CalculateSalaryAmount(workTimesPerMonth);
        }
    }
}