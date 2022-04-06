using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Timesheets.Domain;

namespace Timesheets.API.Controllers
{
    /// <summary>
    /// ProjectsController
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(IProjectsService projectsService, ILogger<ProjectsController> logger)
        {
            _projectsService = projectsService;
            this.logger = logger;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> Get(int projectId)
        {
            return Ok();
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <remarks>Test message</remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <remarks>Test message</remarks>
        /// <returns></returns>
        [ApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> CreateV2()
        {
            return Ok();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            return Ok();
        }
    }
}
