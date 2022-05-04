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
    public class WorkTimesController : ControllerBase
    {
        private readonly IWorkTimesService _workTimesService;
        private readonly ILogger _logger;

        public WorkTimesController(IWorkTimesService workTimesService, ILogger<WorkTimesController> logger)
        {
            _workTimesService = workTimesService;
            _logger = logger;
        }

        /// <summary>
        /// Get workTimes.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int employeeId)
        {
            var workTimes = await _workTimesService.Get(employeeId);

            return Ok(workTimes);
        }

        /// <summary>
        /// Add workTime.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="projectId"></param>
        /// <param name="newWorkTime"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(int employeeId, int projectId, [FromBody] NewWorkTime newWorkTime)
        {
            var (workTime, errors) = WorkTime.Create(employeeId, projectId, newWorkTime.Hours, newWorkTime.Date);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var result = await _workTimesService.Create(workTime);

            return Ok(result);
        }
    }
}