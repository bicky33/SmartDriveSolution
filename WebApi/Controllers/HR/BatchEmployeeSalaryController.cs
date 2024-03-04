using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.RequestFeatured;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.HR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchEmployeeSalaryController : ControllerBase
    {
        private readonly IServiceHRManager _serviceManager;

        public BatchEmployeeSalaryController(IServiceHRManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<BatchEmployeeSalaryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchEmployeeSalary>>> GetBesa()
        {
            var besa = await _serviceManager.BatchEmployeeSalaryService.GetAllAsync(false);

            return Ok(besa);
        }

        // GET api/<BatchEmployeeSalaryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchEmployeeSalary>> GetBesaById(int id)
        {
            var besa = await _serviceManager.BatchEmployeeSalaryService.GetByIdAsync(id, false);
            return Ok(besa);
        }

        // POST api/<BatchEmployeeSalaryController>
        [HttpPost]
        public async Task<IActionResult> CreateBesa([FromBody] BatchEmployeeSalaryDto besaDto)
        {
            var besa = await _serviceManager.BatchEmployeeSalaryService.CreateAsync(besaDto);

            return CreatedAtAction(nameof(GetBesaById), new { id = besa.BesaEmpEntityId }, besa);
        }

        // PUT api/<BatchEmployeeSalaryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBesa(int id, [FromBody] BatchEmployeeSalaryDto besaDto)
        {

            await _serviceManager.BatchEmployeeSalaryService.UpdateAsync(id, besaDto);

            return Ok(besaDto);
        }

        // DELETE api/<BatchEmployeeSalaryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.BatchEmployeeSalaryService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<BatchEmployeeSalary>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var besa = await _serviceManager.BatchEmployeeSalaryService.GetAllPagingAsync(entityParameter, false);

            return Ok(besa);
        }
    }
}
