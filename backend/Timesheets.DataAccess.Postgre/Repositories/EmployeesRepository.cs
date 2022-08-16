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

            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<string> AddProjectToEmployee(int employeeId, int projectId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new string("Project not found with this id.");
            }

            if (employee == null)
            {
                return new string("Employee not found with this id.");
            }

            employee.Projects.Add(project);

            await _context.SaveChangesAsync();

            return string.Empty;
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