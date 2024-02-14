using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CityController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCity()
        {
            var city = await _serviceManagerMaster.CityService.GetAllAsync(false);
            return Ok(city);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}", Name = "GetCityByID")]
        public async Task<ActionResult<City>> GetCityByID(int id)
        {
            var city = await _serviceManagerMaster.CityService.GetByIdAsync(id, false);
            return Ok(city);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityResponse request)
        {
            if (request == null)
            {
                return BadRequest("City Request is NOT valid");
            }
            var city = await _serviceManagerMaster.CityService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCityByID), new { id = city.CityId }, city);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityResponse request)
        {
            await _serviceManagerMaster.CityService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetCityByID), new { id = request.CityId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _serviceManagerMaster.CityService.DeleteAsync(id);

            return NoContent();
        }
    }
}