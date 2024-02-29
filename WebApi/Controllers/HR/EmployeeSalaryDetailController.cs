using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.HR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryDetailController : ControllerBase
    {
        private readonly IServiceHRManager _serviceManager;

        public EmployeeSalaryDetailController(IServiceHRManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<EmployeeSalaryDetailController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSalaryDetail>>> GetEmsa()
        {
            var emsa = await _serviceManager.EmployeeSalaryDetailService.GetAllAsync(false);

            return Ok(emsa);
        }

        // GET api/<EmployeeSalaryDetailController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSalaryDetail>> GetEmsaById(int id)
        {
            var emsa = await _serviceManager.EmployeeSalaryDetailService.GetByIdAsync(id, false);
            return Ok(emsa);
        }

        // POST api/<EmployeeSalaryDetailController>
        [HttpPost]
        public async Task<IActionResult> CreateBesa([FromBody] EmployeeSalaryDetailDto emsaDto)
        {
            var emsa = await _serviceManager.EmployeeSalaryDetailService.CreateAsync(emsaDto);

            return CreatedAtAction(nameof(GetEmsaById), new { id = emsa.EmsaId }, emsa);
        }

        // PUT api/<EmployeeSalaryDetailController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBesa(int id, [FromBody] EmployeeSalaryDetailDto emsaDto)
        {

            await _serviceManager.EmployeeSalaryDetailService.UpdateAsync(id, emsaDto);

            return Ok(emsaDto);
        }

        // DELETE api/<EmployeeSalaryDetailController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.EmployeeSalaryDetailService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<EmployeeSalaryDetail>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var emsa = await _serviceManager.EmployeeSalaryDetailService.GetAllPagingAsync(entityParameter, false);

            return Ok(emsa);
        }
    }
}
