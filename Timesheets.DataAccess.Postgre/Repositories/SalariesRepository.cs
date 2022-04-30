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

        public async Task<Domain.Salary[]> Get()
        {
            var salaryEntities = await _context.Salaries
                .AsNoTracking()
                .ToArrayAsync();

            var salaries = _mapper.Map<Salary[], Domain.Salary[]>(salaryEntities);

            return salaries;
        }
    }
}