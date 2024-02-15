using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplateInsurancePremiController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public TemplateInsurancePremiController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateInsurancePremi>>> GetAllTemplateInsurancePremi()
        {
            var temi = await _serviceManagerMaster.TemplateInsurancePremiService.GetAllAsync(false);
            return Ok(temi);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateInsurancePremi>> GetTemplateInsurancePremiByID(int id)
        {
            var temi = await _serviceManagerMaster.TemplateInsurancePremiService.GetByIdAsync(id, false);
            return Ok(temi);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateTemplateInsurancePremi([FromBody] TemplateInsurancePremiResponse request)
        {
            if (request == null)
            {
                return BadRequest("TemplateInsurancePremi Request is NOT valid");
            }
            var temi = await _serviceManagerMaster.TemplateInsurancePremiService.CreateAsync(request);
            return CreatedAtAction(nameof(GetTemplateInsurancePremiByID), new { id = temi.TemiId }, temi);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateInsurancePremi(int id, [FromBody] TemplateInsurancePremiResponse request)
        {
            await _serviceManagerMaster.TemplateInsurancePremiService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetTemplateInsurancePremiByID), new { id = request.TemiId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateInsurancePremi(int id)
        {
            await _serviceManagerMaster.TemplateInsurancePremiService.DeleteAsync(id);

            return NoContent();
        }
    }
}