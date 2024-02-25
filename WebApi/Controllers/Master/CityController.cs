using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CityController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            var city = await _serviceManagerMaster.CityService.GetAllAsync(false);
            return Ok(city);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int id)
        {
            var city = await _serviceManagerMaster.CityService.GetByIdAsync(id, false);
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityResponse request)
        {
            if (request == null)
            {
                return BadRequest("City Request is NOT valid");
            }
            var city = await _serviceManagerMaster.CityService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = city.CityId }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityResponse request)
        {
            await _serviceManagerMaster.CityService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(Get), new { id = request.CityId }, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.CityService.DeleteAsync(id);

            return NoContent();
        }
    }
}