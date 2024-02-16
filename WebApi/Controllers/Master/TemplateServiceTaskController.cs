using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplateServiceTaskController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public TemplateServiceTaskController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateServiceTask>>> Get()
        {
            var templateServiceTask = await _serviceManagerMaster.TemplateServiceTaskService.GetAllAsync(false);
            return Ok(templateServiceTask);
        }
        // GET: api/<CarSeriesController>
        [HttpGet("testa/{id}")]
        public async Task<ActionResult<IEnumerable<TemplateServiceTask>>> GetTesta(int id)
        {
            var templateServiceTask = await _serviceManagerMaster.TemplateServiceTaskService.GetAllTestaAsync(id,false);
            return Ok(templateServiceTask);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateServiceTask>> Get(int id)
        {
            var templateServiceTask = await _serviceManagerMaster.TemplateServiceTaskService.GetByIdAsync(id, false);
            return Ok(templateServiceTask);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateTemplateServiceTask([FromBody] TemplateServiceTaskResponse request)
        {
            if (request == null)
            {
                return BadRequest("TemplateServiceTask Request is NOT valid");
            }
            var templateServiceTask = await _serviceManagerMaster.TemplateServiceTaskService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = templateServiceTask.TestaId }, templateServiceTask);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TemplateServiceTaskResponse request)
        {
            await _serviceManagerMaster.TemplateServiceTaskService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(Get), new { id = request.TestaId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.TemplateServiceTaskService.DeleteAsync(id);

            return NoContent();
        }
    }
}