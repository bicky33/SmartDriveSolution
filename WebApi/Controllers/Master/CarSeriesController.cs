using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class CarSeriesController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CarSeriesController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarSeries>>> Get()
        {
            var carSeries = await _serviceManagerMaster.CarSeriesService.GetAllAsync(false);
            return Ok(carSeries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarSeries>> Get(int id)
        {
            var carSeries = await _serviceManagerMaster.CarSeriesService.GetByIdAsync(id, false);
            return Ok(carSeries);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarSeriesResponse request)
        {
            if (request == null)
            {
                return BadRequest("Car Brand Request is NOT valid");
            }
            var carSeries = await _serviceManagerMaster.CarSeriesService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = carSeries.CarsId }, carSeries);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarSeriesResponse request)
        {
            await _serviceManagerMaster.CarSeriesService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.CarSeriesService.DeleteAsync(id);

            return NoContent();
        }
    }
}