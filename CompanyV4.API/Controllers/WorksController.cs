using CompanyV4.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyV4.API.Controllers
{
    [ApiController]
    [Route("api/work")]
    public class WorksOnController : ControllerBase
    {
        private readonly IWorksOnService _worksOnService;

        public WorksOnController(IWorksOnService worksOnService)
        {
            _worksOnService = worksOnService;
        }

        // Endpoint untuk mendapatkan semua entri WorksOn
        [HttpGet]
        public IActionResult GetAllWorksOns()
        {
            var worksOns = _worksOnService.GetAllWorksOns();
            return Ok(worksOns);
        }

        // Endpoint untuk mendapatkan satu entri WorksOn berdasarkan ID
        [HttpGet("{id}")]
        public IActionResult GetWorksOnById(int id)
        {
            var worksOn = _worksOnService.GetWorksOnById(id);
            if (worksOn == null)
            {
                return NotFound("WorksOn entry not found.");
            }
            return Ok(worksOn);
        }

        // Endpoint untuk memperbarui HoursWorked pada entri WorksOn
        [HttpPut("update-hoursworked/{id}")]
        public async Task<IActionResult> UpdateHoursWorked(int id, [FromBody] int hoursWorked)
        {
            var result = await _worksOnService.UpdateHoursWorkedAsync(id, hoursWorked);
            if (result == "WorksOn entry not found.")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
