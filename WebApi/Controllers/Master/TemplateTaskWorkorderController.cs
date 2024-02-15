using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplateTaskWorkorderController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public TemplateTaskWorkorderController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateTaskWorkorder>>> GetAllTemplateTaskWorkorder()
        {
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
            return Ok(templateTaskWorkorder);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateTaskWorkorder>> GetTemplateTaskWorkorderByID(int id)
        {
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.GetByIdAsync(id, false);
            return Ok(templateTaskWorkorder);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateTemplateTaskWorkorder([FromBody] TemplateTaskWorkorderResponse request)
        {
            if (request == null)
            {
                return BadRequest("TemplateTaskWorkorder Request is NOT valid");
            }
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.CreateAsync(request);
            return CreatedAtAction(nameof(GetTemplateTaskWorkorderByID), new { id = templateTaskWorkorder.TewoId }, templateTaskWorkorder);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplateTaskWorkorder(int id, [FromBody] TemplateTaskWorkorderResponse request)
        {
            await _serviceManagerMaster.TemplateTaskWorkorderService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetTemplateTaskWorkorderByID), new { id = request.TewoId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateTaskWorkorder(int id)
        {
            await _serviceManagerMaster.TemplateTaskWorkorderService.DeleteAsync(id);

            return NoContent();
        }
    }
}