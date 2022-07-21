using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    public class WorkTimesController : BaseController
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
    }
}