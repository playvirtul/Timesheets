using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using Timesheets.Domain.Auth;

namespace Timesheets.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        protected Result<Role> UserRole
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                if (claim == null)
                {
                    return Result.Failure<Role>($"{nameof(claim)} cannot be null");
                }

                return (Role)Enum.Parse(typeof(Role), claim.Value);
            }
        }
    }
}