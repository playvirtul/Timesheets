using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeesService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Get employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.Get();

            return Ok(employees);
        }

        /// <summary>
        /// Add employee.
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(NewEmployee newEmployee)
        {
            var (employee, errors) = Employee.Create(newEmployee.FirstName, newEmployee.LastName, newEmployee.Position);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            await _employeeService.Add(employee);

            return Ok();
        }
    }
}