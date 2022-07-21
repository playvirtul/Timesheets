using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IInvitationService _invitationService;
        private readonly IUsersService _usersService;
        private readonly IEmployeesService _employeesService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IInvitationService invitationService,
            IUsersService usersService,
            IEmployeesService employeesService,
            ILogger<UsersController> logger)
        {
            _invitationService = invitationService;
            _usersService = usersService;
            _employeesService = employeesService;
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetInvitation([FromQuery]string code)
        //{
        //    var invitation = await _invitationService.Get(code);

        //    return Ok(invitation);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUser newUser, [FromQuery]string code)
        {
            var invitation = await _invitationService.Get(code);

            if (invitation == null)
            {
                return BadRequest("Приглашение не найдено");
            }

            var (user, userErrors) = Domain.User.Create(newUser.Email, newUser.Password, newUser.Role);

            if (userErrors.Any())
            {
                _logger.LogError("{errors}", userErrors);
                return BadRequest(userErrors);
            }

            var existingUser = await _usersService.Get(newUser.Email);

            if (existingUser != null)
            {
                return BadRequest("Пользователь с такой учетной записью уже существует");
            }

            var userId = await _usersService.Create(user, code);

            var (employee, employeeErrors) = Employee
                .Create(userId, invitation.FirstName, invitation.LastName, invitation.Position);

            if (employeeErrors.Any())
            {
                _logger.LogError("{errors}", userErrors);
                return BadRequest(userErrors);
            }

            await _employeesService.Create(employee);

            var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.NameIdentifier, userId)
                      .WithVerifySignature(true)
                      .Encode();

            return Ok(token);
        }

        [HttpGet("validation")]
        public async Task<IActionResult> Validate(string token)
        {
            var json = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .MustVerifySignature()
                      .Decode(token);

            return Ok(json);
        }

        [Authorize]
        [HttpGet("validation2")]
        public async Task<IActionResult> Validate2(string token)
        {
            var json = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .MustVerifySignature()
                      .Decode(token);

            return Ok(json);
        }
    }
}