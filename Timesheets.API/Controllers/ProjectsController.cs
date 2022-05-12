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
    /// ProjectsController.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IWorkTimesService _workTimesService;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(
            IProjectsService projectsService,
            IWorkTimesService workTimeService,
            ILogger<ProjectsController> logger)
        {
            _projectsService = projectsService;
            _workTimesService = workTimeService;
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
        /// AddEmployeeToProject.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost("{projectId:int}/employee")]
        public async Task<IActionResult> AddEmployeeToProject([FromRoute]int projectId, [FromBody]int employeeId)
        {
            var error = await _projectsService.AddEmployeeToProject(projectId, employeeId);

            if (error != string.Empty)
            {
                _logger.LogError("{error}", error);
            }

            return Ok(error);
        }

        [HttpPost("{projectId:int}/workTime")]
        public async Task<IActionResult> CreateWorkTime(
            [FromQuery]int employeeId,
            [FromRoute]int projectId,
            [FromBody] NewWorkTime newWorkTime)
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
    }
}