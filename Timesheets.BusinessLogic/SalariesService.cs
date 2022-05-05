using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class SalariesService : ISalariesService
    {
        private readonly ISalariesRepository _salariesRepository;

        public SalariesService(ISalariesRepository salariesRepository)
        {
            _salariesRepository = salariesRepository;
        }

        public async Task<Salary> Get(int employeeId)
        {
            return await _salariesRepository.Get(employeeId);
        }

        public async Task Upsert(Salary salary)
        {
            await _salariesRepository.Upsert(salary);
        }
    }
}