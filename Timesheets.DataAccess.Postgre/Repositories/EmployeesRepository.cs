using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.DataAccess.Postgre.Entities;
using Timesheets.Domain.Interfaces;

namespace Timesheets.DataAccess.Postgre.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly TimesheetsDbContext _context;
        private readonly IMapper _mapper;

        public EmployeesRepository(TimesheetsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Domain.Employee[]> Get()
        {
            var employeeEntities = await _context.Employees
                .Include(e => e.Projects)
                .AsNoTracking()
                .ToArrayAsync();

            var employees = employeeEntities
                .Select(employeeEntity => EmployeeMapping(employeeEntity))
                .ToArray();

            return employees;
        }

        public async Task<Domain.Employee?> Get(int employeeId)
        {
            var employeeEntity = await _context.Employees
                .Include(e => e.Projects)
                .ThenInclude(p => p.WorkTimes)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employeeEntity == null)
            {
                return null;
            }

            var employee = EmployeeMapping(employeeEntity);

            return employee;
        }

        public async Task<int> Add(Domain.Employee newEmployee)
        {
            var employee = _mapper.Map<Domain.Employee, Employee>(newEmployee);

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> Delete(int employeeId)
        {
            _context.Employees.Remove(new Employee { Id = employeeId });

            await _context.SaveChangesAsync();

            return true;
        }

        private Domain.Employee EmployeeMapping(Employee employeeEntity)
        {
            return employeeEntity.Position switch
            {
                Domain.Position.Chief => _mapper.Map<Employee, Domain.Chief>(employeeEntity),
                Domain.Position.StaffEmployee => _mapper.Map<Employee, Domain.StaffEmployee>(employeeEntity),
                Domain.Position.Manager => _mapper.Map<Employee, Domain.Manager>(employeeEntity),
                Domain.Position.Freelancer => _mapper.Map<Employee, Domain.Freelancer>(employeeEntity),
                _ => throw new Exception("Incorrect employee role")
            };
        }
    }
}