using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
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
        private readonly IInvitationsService _invitationsService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EmployeesController(
            IEmployeesService employeesService,
            ISalariesService salariesService,
            IInvitationsService invitationService,
            ILogger<EmployeesController> logger,
            IMapper mapper)
        {
            _employeesService = employeesService;
            _salariesService = salariesService;
            _invitationsService = invitationService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get employees.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeesService.Get();

            var response = _mapper.Map<Employee[], GetEmployeeResponse[]>(employees);

            return Ok(response);
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

            if (salary.IsFailure)
            {
                _logger.LogError("{error}", salary.Error);
                return BadRequest(salary.Error);
            }

            var response = _mapper.Map<Salary, GetSalaryResponse>(salary.Value);

            return Ok(response);
        }

        /// <summary>
        /// Invite employee.
        /// </summary>
        /// <param name="emlpoyeeDetailsRequest"></param>
        /// <returns></returns>
        [HttpPost("telegramInvitation/employeeDetails")]
        public async Task<IActionResult> SendTelegramInvite([FromBody] CreateInvitationRequest emlpoyeeDetailsRequest)
        {
            var telergamInvitation = TelegramInvitation.Create(
                emlpoyeeDetailsRequest.UserName,
                emlpoyeeDetailsRequest.FirstName,
                emlpoyeeDetailsRequest.LastName,
                emlpoyeeDetailsRequest.Position,
                emlpoyeeDetailsRequest.Role);

            if (telergamInvitation.IsFailure)
            {
                _logger.LogError("{errors}", telergamInvitation.Error);
                return BadRequest(telergamInvitation.Error);
            }

            var result = await _employeesService.SendTelegramInvite(telergamInvitation.Value);

            if (result.IsFailure)
            {
                _logger.LogError("{error}", result.Error);
                return BadRequest(result.Error);
            }

            await _invitationsService.Create(telergamInvitation.Value);

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
        /// <param name="salaryRequest"></param>
        /// <returns></returns>
        [HttpPost("{employeeId:int}/salary")]
        public async Task<IActionResult> SaveSalary([FromRoute] int employeeId, [FromBody] CreateSalaryRequest salaryRequest)
        {
            var salary = Salary.Create(employeeId, salaryRequest.Amount, salaryRequest.Bonus, salaryRequest.SalaryType);

            if (salary.IsFailure)
            {
                _logger.LogError("{errors}", salary.Error);
                return BadRequest(salary.Error);
            }

            var salaryId = await _salariesService.Save(salary.Value);

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