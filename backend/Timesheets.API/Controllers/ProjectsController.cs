using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API.Controllers
{
    /// <summary>
    /// ProjectsController.
    /// </summary>
    public class ProjectsController : BaseController
    {
        private readonly IProjectsService _projectsService;
        private readonly IWorkTimesService _workTimesService;
        private readonly ILogger<ProjectsController> _logger;
        private readonly IMapper _mapper;

        public ProjectsController(
            IProjectsService projectsService,
            IWorkTimesService workTimeService,
            ILogger<ProjectsController> logger,
            IMapper mapper)
        {
            _projectsService = projectsService;
            _workTimesService = workTimeService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get projects.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _projectsService.Get();
            var response = _mapper.Map<Project[], GetProjectResponse[]>(projects);

            return Ok(response);
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

            if (project.IsFailure)
            {
                _logger.LogError("{errors}", project.Error);
                return BadRequest(project.Error);
            }

            var response = _mapper.Map<Project, GetProjectResponse>(project.Value);

            return Ok(response);
        }

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest projectRequest)
        {
            var project = Project.Create(projectRequest.Title);

            if (project.IsFailure)
            {
                _logger.LogError("{errors}", project.Error);
                return BadRequest(project.Error);
            }

            var projectId = await _projectsService.Create(project.Value);

            return Ok(projectId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workTimeRequest"></param>
        /// <returns></returns>
        [HttpPost("{projectId:int}/workTime")]
        public async Task<IActionResult> AddWorkTime([FromRoute]int projectId, [FromBody] CreateWorkTimeRequest workTimeRequest)
        {
            var workTime = WorkTime.Create(
                workTimeRequest.EmployeeId,
                projectId,
                workTimeRequest.Hours,
                workTimeRequest.Date);

            if (workTime.IsFailure)
            {
                _logger.LogError("{errors}", workTime.Error);
                return BadRequest(workTime.Error);
            }

            var result = await _workTimesService.Add(workTime.Value);

            if (result.IsFailure)
            {
                _logger.LogError("{error}", result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
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