using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        [HttpPost("registrate")]
        public async Task<IActionResult> CreateUser(UserRequest userRequest, [FromQuery]string code)
        {
            var invitation = await _invitationService.Get(code);

            if (invitation.IsFailure)
            {
                _logger.LogError("{error}", invitation.Error);
                return BadRequest(invitation.Error);
            }

            var userToCreate = Domain.User.Create(userRequest.Email, userRequest.Password, invitation.Value.Role);

            if (userToCreate.IsFailure)
            {
                _logger.LogError("{errors}", userToCreate.Error);
                return BadRequest(userToCreate.Error);
            }

            var userId = await _usersService.Create(userToCreate.Value, code);

            if (userId.IsFailure)
            {
                _logger.LogError("{errors}", userId.Error);

                return BadRequest(userId.Error);
            }

            var employee = Employee.Create(
                userId.Value,
                invitation.Value.FirstName,
                invitation.Value.LastName,
                invitation.Value.Position);

            if (employee.IsFailure)
            {
                _logger.LogError("{error}", employee.Error);
                return BadRequest(employee.Error);
            }

            await _employeesService.Create(employee.Value);

            var token = GenerateJWT(userToCreate.Value);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginInfo)
        {
            var user = await _usersService.AuthenticateUser(loginInfo.Email, loginInfo.Password);

            if (user.IsFailure)
            {
                _logger.LogError("{error}", user.Error);
                return Unauthorized();
            }

            var token = GenerateJWT(user.Value);
            return Ok(token);
        }

        [HttpGet("validation")]
        public async Task<IActionResult> Validate(string token)
        {
            var json = DecodeJWT(token);

            return Ok(json);
        }

        [Authorize]
        [HttpGet("validation2")]
        public async Task<IActionResult> Validate2(string token)
        {
            var json = DecodeJWT(token);

            return Ok(json);
        }

        public string GenerateJWT(User user)
        {
            var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.NameIdentifier, user.Id)
                      .AddClaim(ClaimTypes.Email, user.Email)
                      .AddClaim(ClaimTypes.Role, user.Role)
                      .WithVerifySignature(true)
                      .Encode();

            return token;
        }

        public string DecodeJWT(string token)
        {
            var json = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .MustVerifySignature()
                      .Decode(token);

            return json;
        }
    }
}