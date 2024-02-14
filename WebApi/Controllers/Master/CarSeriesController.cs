using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarSeriesController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CarSeriesController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarSeries>>> GetAllCarSeries()
        {
            var carSeries = await _serviceManagerMaster.CarSeriesService.GetAllAsync(false);
            return Ok(carSeries);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}", Name = "GetCarSeriesByID")]
        public async Task<ActionResult<CarSeries>> GetCarSeriesByID(int id)
        {
            var carSeries = await _serviceManagerMaster.CarSeriesService.GetByIdAsync(id, false);
            return Ok(carSeries);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateCarSeries([FromBody] CarSeriesResponse request)
        {
            if (request == null)
            {
                return BadRequest("Car Brand Request is NOT valid");
            }
            var carSeries = await _serviceManagerMaster.CarSeriesService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCarSeriesByID), new { id = carSeries.CarsId }, carSeries);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarSeries(int id, [FromBody] CarSeriesResponse request)
        {
            await _serviceManagerMaster.CarSeriesService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarSeries(int id)
        {
            await _serviceManagerMaster.CarSeriesService.DeleteAsync(id);

            return NoContent();
        }
    }
}