using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class WorkTimesRepository : IWorkTimesRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly Mapper _mapper;

        public WorkTimesRepository(TimesheetsDbContext context, Mapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.WorkTime[]> Get()
        {
            var workTimesEntities = await _context.WorkTimes
                .AsNoTracking()
                .ToArrayAsync();

            var workTimes = _mapper.Map<WorkTime[], Domain.WorkTime[]>(workTimesEntities);

            return workTimes;
        }

        public async Task<int> Create(Domain.WorkTime newWorkTime)
        {
            var workTime = _mapper.Map<Domain.WorkTime, WorkTime>(newWorkTime);

            await _context.WorkTimes.AddAsync(workTime);

            return workTime.Id;
        }
    }
}