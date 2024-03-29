﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Domain.WorkTime[]> Get(int employeeId)
        {
            var workTimesEntities = await _context.WorkTimes
                .AsNoTracking()
                .Where(w => w.EmployeeId == employeeId)
                .ToArrayAsync();

            var workTimes = _mapper.Map<WorkTime[], Domain.WorkTime[]>(workTimesEntities);

            return workTimes;
        }

        public async Task<Domain.WorkTime[]> Get(int employeeId, int month, int year)
        {
            var workTimesEntities = await _context.WorkTimes
                .AsNoTracking()
                .Where(w => w.EmployeeId == employeeId)
                .Where(w => w.Date.Year == year)
                .Where(w => w.Date.Month == month)
                .ToArrayAsync();

            var workTimes = _mapper.Map<WorkTime[], Domain.WorkTime[]>(workTimesEntities);

            return workTimes;
        }

        public async Task<int> Add(Domain.WorkTime newWorkTime)
        {
            var workTime = _mapper.Map<WorkTime>(newWorkTime);

            await _context.WorkTimes.AddAsync(workTime);

            await _context.SaveChangesAsync();

            return workTime.EmployeeId;
        }
    }
}