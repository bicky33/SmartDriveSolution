using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class TemplateTaskWorkorderController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public TemplateTaskWorkorderController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateTaskWorkorder>>> Get()
        {
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.GetAllAsync(false);
            return Ok(templateTaskWorkorder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateTaskWorkorder>> Get(int id)
        {
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.GetByIdAsync(id, false);
            return Ok(templateTaskWorkorder);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateTaskWorkorderResponse request)
        {
            if (request == null)
            {
                return BadRequest("TemplateTaskWorkorder Request is NOT valid");
            }
            var templateTaskWorkorder = await _serviceManagerMaster.TemplateTaskWorkorderService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = templateTaskWorkorder.TewoId }, templateTaskWorkorder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TemplateTaskWorkorderResponse request)
        {
            await _serviceManagerMaster.TemplateTaskWorkorderService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.TemplateTaskWorkorderService.DeleteAsync(id);

            return NoContent();
        }
    }
}