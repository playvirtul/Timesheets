using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                .AsNoTracking()
                .ToArrayAsync();

            var employees = _mapper.Map<Employee[], Domain.Employee[]>(employeeEntities);

            return employees;
        }

        public async Task<Domain.Employee?> Get(int employeeId)
        {
            var employeeEntity = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employeeEntity == null)
            {
                return null;
            }

            var employee = _mapper.Map<Employee, Domain.Employee>(employeeEntity);

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
    }
}