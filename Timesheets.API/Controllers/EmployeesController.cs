using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

        [HttpGet("exception")]
        public async Task<IActionResult> Exception()
        {
            Test();

            throw new Exception(Guid.NewGuid().ToString());
        }

        public void Test()
        {
            var source = new ActivitySource("Timesheets.API");
            using (var activity = source.StartActivity("start-expeption"))
            {
                activity.SetTag("test", 100);
            }
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
        /// Get salary by employeeId.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId:int}/salary")]
        public async Task<IActionResult> Get(int employeeId)
        {
            var salary = await _salariesService.Get(employeeId);

            return Ok(salary);
        }

        /// <summary>
        /// Add employee.
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewEmployee newEmployee)
        {
            var chief = Chief.Create("name", "lastname").Result;

            var (employee, errors) = chief.Create(newEmployee.FirstName, newEmployee.LastName, newEmployee.Position);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var employeeId = await _employeesService.Create(employee);

            return Ok(employeeId);
        }

        /// <summary>
        /// Add employee to project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost("{employeeId:int}/project")]
        public async Task<IActionResult> BindProject([FromRoute] int employeeId, [FromQuery] int projectId)
        {
            var error = await _employeesService.BindProject(employeeId, projectId);

            if (error != string.Empty)
            {
                _logger.LogError("{error}", error);
            }

            return Ok(error);
        }

        /// <summary>
        /// Create or update salary.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="newSalary"></param>
        /// <returns></returns>
        [HttpPost("{employeeId:int}/salary")]
        public async Task<IActionResult> Upsert([FromRoute] int employeeId, [FromBody] NewSalary newSalary)
        {
            var (salary, errors) = Salary.Create(employeeId, newSalary.Amount, newSalary.Bonus, newSalary.SalaryType);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var salaryId = await _salariesService.Save(salary);

            return Ok(salaryId);
        }

        /// <summary>
        /// Calculate salary for time period.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("{employeeId:int}/salary-calculation")]
        public async Task<IActionResult> SalaryCalculation(
            [FromRoute] int employeeId,
            [FromQuery, Range(1, 12)] int month,
            [FromQuery] int year)
        {
            var amountSalary = await _salariesService.SalaryCalculation(employeeId, month, year);

            return Ok(amountSalary);
        }
    }
}