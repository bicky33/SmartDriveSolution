using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public ZoneController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> Get()
        {
            var categories = await _serviceManagerMaster.ZoneService.GetAllAsync(false);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> Get(int id)
        {
            var category = await _serviceManagerMaster.ZoneService.GetByIdAsync(id, false);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ZoneResponse request)
        {
            if (request == null)
            {
                return BadRequest("Zone Request is NOT valid");
            }
            var category = await _serviceManagerMaster.ZoneService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = category.ZonesId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ZoneResponse request)
        {
            await _serviceManagerMaster.ZoneService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.ZoneService.DeleteAsync(id);

            return NoContent();
        }
    }
}