using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    /// <summary>
    /// Employee controller.
    /// </summary>
    public class EmployeesController : BaseController
    {
        private readonly IEmployeesService _employeesService;
        private readonly ISalariesService _salariesService;
        private readonly IInvitationService _invitationService;
        private readonly ILogger _logger;

        public EmployeesController(
            IEmployeesService employeesService,
            ISalariesService salariesService,
            IInvitationService invitationService,
            ILogger<EmployeesController> logger)
        {
            _employeesService = employeesService;
            _salariesService = salariesService;
            _invitationService = invitationService;
            _logger = logger;
        }

        /// <summary>
        /// Get employees.
        /// </summary>
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
        /// Invite employee.
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        [HttpPost("telegramInvitation/employeeDetails")]
        public async Task<IActionResult> SendTelegramInvite([FromBody] TelegramEmlpoyeeDetails employeeDetails)
        {
            var (telergamInvitation, errors) = TelegramInvitation.Create(
                employeeDetails.TelegramUserName,
                employeeDetails.FirstName,
                employeeDetails.LastName,
                employeeDetails.Position);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var result = await _employeesService.SendTelegramInvite(telergamInvitation);

            if (result == false)
            {
                return BadRequest();
            }

            await _invitationService.Create(telergamInvitation);

            return Ok();
        }

        /// <summary>
        /// Add employee to project.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="projectId"></param>
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
        public async Task<IActionResult> SaveSalary([FromRoute] int employeeId, [FromBody] NewSalary newSalary)
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
        /// <param name="year"></param>
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