using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.BusinessLogic
{
    public class EmployeeService : IEmployeeService
    {
        public async Task Add(Employee employee)
        {
            Employees.Add(employee);
        }

        public async Task<Employee[]> Get()
        {
            return Employees.Get();
        }
    }

    public static class Employees
    {
        private static List<Employee> _employeeList = new List<Employee>();

        public static void Add(Employee employee)
        {
            _employeeList.Add(employee);
        }

        public static Employee[] Get()
        {
            return _employeeList.ToArray();
        }
    }
}