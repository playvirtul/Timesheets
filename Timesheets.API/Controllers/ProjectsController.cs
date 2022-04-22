using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

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
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectsService projectsService, ILogger<ProjectsController> logger)
        {
            _projectsService = projectsService;
            _logger = logger;
        }

        /// <summary>
        /// Get projects.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _projectsService.Get();

            return Ok(projects);
        }

        /// <summary>
        /// Get project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> Get(int projectId)
        {
            var project = await _projectsService.Get(projectId);

            return Ok(project);
        }

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="newProject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]NewProject newProject)
        {
            var (project, errors) = Project.Create(newProject.Title);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var projectId = await _projectsService.Create(project);

            return Ok(projectId);
        }

        /// <summary>
        /// Delete project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var deletedProjectId = await _projectsService.Delete(projectId);

            return Ok(deletedProjectId);
        }

        /// <summary>
        /// AddWorkTime.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="newWorkTime"></param>
        /// <returns></returns>
        [HttpPost("{projectId:int}")]
        public async Task<IActionResult> AddWorkTime(int projectId, [FromBody]NewWorkTime newWorkTime)
        {
            var (workTime, errors) = WorkTime.Create(projectId, newWorkTime.Hours, newWorkTime.Date);

            if (errors.Any())
            {
                _logger.LogError("{errors}", errors);
                return BadRequest(errors);
            }

            var result = await _projectsService.AddWorkTime(workTime);

            return Ok(result);
        }
    }
}