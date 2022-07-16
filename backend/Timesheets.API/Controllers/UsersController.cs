using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Timesheets.API.Contracts;

namespace Timesheets.API.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetInvitation([FromQuery]string code)
        {
            // найти приглашение в базе
            // вернуть данные сотрудника

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUser newUser)
        {
            // создаём доменного юзера
            // проверяем, что нет юзера с таким email
            // создаем пользователя и сотрудника
            // вернуть токен авторизации

            var userId = 1;

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