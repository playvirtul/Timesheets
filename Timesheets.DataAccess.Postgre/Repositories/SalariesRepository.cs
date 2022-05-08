using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class SalariesRepository : ISalariesRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public SalariesRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.Salary?> Get(int employeeId)
        {
            var salaryEntity = await _context.Salaries
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId);

            if (salaryEntity == null)
            {
                return null;
            }

            var salary = _mapper.Map<Salary, Domain.Salary>(salaryEntity);

            return salary;
        }

        public async Task Upsert(Domain.Salary salary)
        {
            var entitySalary = _mapper.Map<Domain.Salary, Salary>(salary);

            _context.Salaries.Update(entitySalary);

            await _context.SaveChangesAsync();
        }
    }
}