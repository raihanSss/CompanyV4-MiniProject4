using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyV4.API.Controllers
{
    [Route("api/emp")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            var employees = _employeeService.GetAllEmployee();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee object is null");
            }

            var createdEmployee = await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmpNo }, createdEmployee);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateEmployee(int id, Employee employee)
        {

            var existingEmployee = _employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound("Employee tidak ditemukan");
            }

            var result = _employeeService.UpdateEmployee(id, employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteEmployee(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            if (result.Contains("Employee tidak ditemukan"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [HttpGet("female-birth-years")]
        public ActionResult<IEnumerable<int>> GetFemaleEmployeeBirthYearsAfter1990()
        {
            var tahunLahir = _employeeService.GetFemaleEmployeeBirthYearsAfter1990();
            return Ok(tahunLahir);
        }

        [HttpGet("brics")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesFromBrics()
        {
            var employees = _employeeService.GetEmployeesFromBrics();
            return Ok(employees);
        }

        [HttpGet("born-1980-1990")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesBornBetween1980And1990()
        {
            var employees = _employeeService.GetEmployeesBornBetween1980And1990();
            return Ok(employees);
        }

        [HttpGet("non-managers")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesNotManagers()
        {
            var employees = _employeeService.GetEmployeesNotManagers();
            return Ok(employees);
        }

        [HttpGet("it-department")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesInITDepartment()
        {
            var employees = _employeeService.GetEmployeesInITDepartment();
            return Ok(employees);
        }

        [HttpGet("employees-age")]
        public ActionResult<IEnumerable<object>> GetEmployeesWithAge()
        {
            var employeesWithAge = _employeeService.GetEmployeesWithAge();
            return Ok(employeesWithAge);
        }
    }
}
