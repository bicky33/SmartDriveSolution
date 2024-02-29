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
    public class TemplateSalaryController : ControllerBase
    {
        private readonly IServiceHRManager _serviceManager;

        public TemplateSalaryController(IServiceHRManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateSalary>>> GetTesa()
        {
            var besa = await _serviceManager.TemplateSalaryService.GetAllAsync(false);

            return Ok(besa);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateSalary>> GetTesaById(int id)
        {
            var besa = await _serviceManager.TemplateSalaryService.GetByIdAsync(id, false);
            return Ok(besa);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBesa([FromBody] TemplateSalaryDto besaDto)
        {
            var besa = await _serviceManager.TemplateSalaryService.CreateAsync(besaDto);

            return CreatedAtAction(nameof(GetTesaById), new { id = besa.TesalId }, besa);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTesa(int id, [FromBody] TemplateSalaryDto besaDto)
        {

            await _serviceManager.TemplateSalaryService.UpdateAsync(id, besaDto);

            return Ok(besaDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.TemplateSalaryService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<TemplateSalary>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var besa = await _serviceManager.TemplateSalaryService.GetAllPagingAsync(entityParameter, false);

            return Ok(besa);
        }
    }
}
