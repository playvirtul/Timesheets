using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly ISalariesService _salariesService;
        private readonly ILogger _logger;

        public EmployeesController(
            IEmployeesService employeesService,
            ISalariesService salariesService,
            ILogger<EmployeesController> logger)
        {
            _employeesService = employeesService;
            _salariesService = salariesService;
            _logger = logger;
        }

        /// <summary>
        /// Get employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeesService.Get();

            return Ok(employees);
        }

        /// <summary>
        /// Add employee.
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]NewEmployee newEmployee)
        {
            var chief = Chief.Create("name", "lastname").Result;

            var (employee, errors) = chief.Create(newEmployee.FirstName, newEmployee.LastName, newEmployee.Position);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var employeeId = await _employeesService.Create(employee);

            var employeeSalary = Salary.Create(employeeId, newEmployee.SalaryType);

            await _salariesService.SetupSalary(employeeSalary);

            return Ok(employeeId);
        }
    }
}