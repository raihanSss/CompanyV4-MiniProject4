using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyV4.API.Controllers
{
    [ApiController]
    [Route("api/proj")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest("Project data is required.");
            }

            var createdProject = await _projectService.CreateProjAsync(project);
            return Ok(createdProject);
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProj();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _projectService.GetProjById(id);
            if (project == null)
            {
                return NotFound("Project not found.");
            }
            return Ok(project);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project project)
        {
            if (project == null || project.ProjNo != id)
            {
                return BadRequest("Project data is invalid.");
            }

            var result = _projectService.UpdateProj(project);
            if (result == "Project not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var result = _projectService.DeleteProj(id);
            if (result == "Project not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("planning-dept-projects")]
        public IActionResult GetProjectsManagedByPlanningDept()
        {
            var projects = _projectService.GetProjectsManagedByPlanningDept();
            return Ok(projects);
        }

        [HttpGet("no-employees-projects")]
        public IActionResult GetProjectsWithNoEmployees()
        {
            var projects = _projectService.GetProjectsWithNoEmployees();
            return Ok(projects);
        }

        [HttpGet("departments-with-more-than-ten-employees")]
        public IActionResult GetDepartmentsWithMoreThanTenEmployees()
        {
            var departments = _projectService.GetDepartmentsWithMoreThanTenEmployees();
            return Ok(departments);
        }

        [HttpGet("female-employees-hours")]
        public IActionResult GetTotalHoursWorkedByFemaleEmployees()
        {
            var report = _projectService.GetTotalHoursWorkedByFemaleEmployees();
            return Ok(report);
        }

        [HttpGet("it-hr-dept-projects")]
        public IActionResult GetProjectsManagedByITAndHRDept()
        {
            var projects = _projectService.GetProjectsManagedByITAndHRDept();
            return Ok(projects);
        }

        [HttpGet("non-managers-supervisors")]
        public IActionResult GetNonManagersAndSupervisors()
        {
            var employees = _projectService.GetNonManagersAndSupervisors();
            return Ok(employees);
        }

        [HttpGet("female-managers-projects")]
        public IActionResult GetFemaleManagersAndTheirProjects()
        {
            var report = _projectService.GetFemaleManagersAndTheirProjects();
            return Ok(report);
        }

        [HttpGet("max-min-hours-worked")]
        public IActionResult GetMaxMinHoursWorkedByEmployee()
        {
            var report = _projectService.GetMaxMinHoursWorkedByEmployee();
            return Ok(report);
        }

        [HttpGet("total-hours-worked")]
        public IActionResult GetTotalHoursWorkedForEachEmployee()
        {
            var report = _projectService.GetTotalHoursWorkedForEachEmployee();
            return Ok(report);
        }
    }
}
