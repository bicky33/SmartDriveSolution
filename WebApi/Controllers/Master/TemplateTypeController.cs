using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplateTypeController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public TemplateTypeController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateType>>> GetAllTemplateType()
        {
            var templateTypes = await _serviceManagerMaster.TemplateTypeService.GetAllAsync(false);
            return Ok(templateTypes);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetTemplateTypeByID(int id)
        {
            var templateType = await _serviceManagerMaster.TemplateTypeService.GetByIdAsync(id, false);
            return Ok(templateType);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateTemplateType([FromBody] TemplateTypeResponse request)
        {
            if (request == null)
            {
                return BadRequest("Zone Request is NOT valid");
            }
            var templateType = await _serviceManagerMaster.TemplateTypeService.CreateAsync(request);
            return CreatedAtAction(nameof(GetTemplateTypeByID), new { id = templateType.TetyId }, templateType);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateType(int id, [FromBody] TemplateTypeResponse request)
        {
            await _serviceManagerMaster.TemplateTypeService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetTemplateTypeByID), new { id = request.TetyId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateType(int id)
        {
            await _serviceManagerMaster.TemplateTypeService.DeleteAsync(id);

            return NoContent();
        }
    }
}