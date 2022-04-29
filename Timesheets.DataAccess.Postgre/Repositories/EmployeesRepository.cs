using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var employees = new List<Domain.Employee>();

            var employeeEntities = await _context.Employees
                .Include(e => e.Salary)
                .AsNoTracking()
                .ToArrayAsync();

            foreach (var item in employeeEntities)
            {
                Domain.Employee? employee = null;

                switch (item.Salary.Position)
                {
                    case (int)Domain.Position.Chief:
                        employee = _mapper.Map<Employee, Domain.Chief>(item);
                        break;

                    case (int)Domain.Position.StuffEmployee:
                        employee = _mapper.Map<Employee, Domain.StuffEmployee>(item);
                        break;

                    case (int)Domain.Position.Manager:
                        employee = _mapper.Map<Employee, Domain.Manager>(item);
                        break;

                    case (int)Domain.Position.Freelancer:
                        employee = _mapper.Map<Employee, Domain.Freelancer>(item);
                        break;
                }

                if (employee != null)
                {
                    employees.Add(employee);
                }
            }

            return employees.ToArray();
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