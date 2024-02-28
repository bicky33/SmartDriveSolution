using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CarBrandController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarBrandResponse>>> Get()
        {
            var carBrands = await _serviceManagerMaster.CarBrandService.GetAllAsync(false);
            return Ok(carBrands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarBrand>> Get(int id)
        {
            var carBrand = await _serviceManagerMaster.CarBrandService.GetByIdAsync(id, false);
            return Ok(carBrand);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarBrandResponse request)
        {
            if (request == null)
            {
                return BadRequest("Car Brand Request is NOT valid");
            }
            var carBrand = await _serviceManagerMaster.CarBrandService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = carBrand.CabrId }, carBrand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarBrandResponse request)
        {
            await _serviceManagerMaster.CarBrandService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.CarBrandService.DeleteAsync(id);

            return NoContent();
        }
    }
}