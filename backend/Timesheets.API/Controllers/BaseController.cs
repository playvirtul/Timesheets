using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Timesheets.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    { }
}