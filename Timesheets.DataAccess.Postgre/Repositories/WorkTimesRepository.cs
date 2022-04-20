using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class WorkTimesRepository : IWorkTimesRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public WorkTimesRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.WorkTime[]> Get()
        {
            var workTimesEntities = await _context.WorkTimes
                .AsNoTracking()
                .ToArrayAsync();

            var workTimes = _mapper.Map<Domain.WorkTime[]>(workTimesEntities);

            return workTimes;
        }

        public async Task<int> Create(Domain.WorkTime newWorkTime)
        {
            var workTime = _mapper.Map<WorkTime>(newWorkTime);

            await _context.WorkTimes.AddAsync(workTime);

            return workTime.Id;
        }
    }
}