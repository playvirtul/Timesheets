using System;
using System.Linq;
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

        public async Task SetupSalary(Employee employee)
        {
            var salaries = await _salariesRepository.Get();
            Salary? salary = null;

            switch (employee)
            {
                case Chief:
                    salary = salaries.SingleOrDefault(s => s.Position == (int)Position.Chief);
                    break;
                case StuffEmployee:
                    salary = salaries.SingleOrDefault(s => s.Position == (int)Position.StuffEmployee);
                    break;
                case Manager:
                    salary = salaries.SingleOrDefault(s => s.Position == (int)Position.Manager);
                    break;
                case Freelancer:
                    salary = salaries.SingleOrDefault(s => s.Position == (int)Position.Freelancer);
                    break;
            }

            if (salary != null)
            {
                employee.SetupSalary(salary);
            }
        }
    }
}