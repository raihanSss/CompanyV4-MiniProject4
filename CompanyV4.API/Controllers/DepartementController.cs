using CompanyV4.Domain;
using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CompanyV4.API.Controllers
{
    [Route("api/dept")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly IOptions<ProjectOptions> _projectOptions;
        private readonly IOptions<EmployeeOptions> _employeeOptions;
        private readonly IOptions<DepartmentOptions> _departmentOptions;
        private readonly IDepartementService _departementService;

        public DepartementController(
            IOptions<ProjectOptions> projectOptions,
            IOptions<EmployeeOptions> employeeOptions,
            IOptions<DepartmentOptions> departmentOptions,
            IDepartementService departementService)
            
        {
            _projectOptions = projectOptions;
            _employeeOptions = employeeOptions;
            _departmentOptions = departmentOptions;
            _departementService = departementService;
        }

        [HttpGet("project-options")]
        public IActionResult GetProjectOptions()
        {
            return Ok(_projectOptions.Value);
        }

        [HttpGet("employee-options")]
        public IActionResult GetEmployeeOptions()
        {
            return Ok(_employeeOptions.Value);
        }

        [HttpGet("department-options")]
        public IActionResult GetDepartmentOptions()
        {
            return Ok(_departmentOptions.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDept([FromBody] Department departement)
        {
            if (departement == null)
            {
                return BadRequest("Departement tidak ada");
            }

            var createdDept = await _departementService.CreateDept(departement);
            return CreatedAtAction(nameof(GetDeptById), new { id = createdDept.DeptNo }, createdDept);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetAllDept()
        {
            var departements = _departementService.GetAllDept();
            return Ok(departements);
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDeptById(int id)
        {
            var departement = _departementService.GetDeptById(id);
            if (departement == null)
            {
                return NotFound("Departement tidak ditemukan");
            }
            return Ok(departement);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateDept(int id, Department departement)
        {
            var existingDept = _departementService.GetDeptById(id);
            if (existingDept == null)
            {
                return NotFound("Department tidak ditemukan");
            }

            var result = _departementService.UpdateDept(id, departement);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteDept(int id)
        {
            var result = _departementService.DeleteDept(id);
            if (result == "Departement tidak ada")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("female-managers")]
        public ActionResult<IEnumerable<Employee>> GetFemaleManagers()
        {
            var femaleManagers = _departementService.GetFemaleManagers();
            return Ok(femaleManagers);
        }

        [HttpGet("count-female-managers")]
        public ActionResult<int> CountFemaleManagers()
        {
            var count = _departementService.CountFemaleManagers();
            return Ok(count);
        }

        [HttpGet("managers-retire")]
        public ActionResult<IEnumerable<Employee>> GetManagersDueToRetireThisYear()
        {
            var managersDueToRetire = _departementService.GetManagersRetire();
            return Ok(managersDueToRetire);
        }

        [HttpGet("managers-under-40")]
        public ActionResult<IEnumerable<Employee>> GetManagersUnder40()
        {
            var managersUnder40 = _departementService.GetManagersUnder40();
            return Ok(managersUnder40);
        }
    }
}

