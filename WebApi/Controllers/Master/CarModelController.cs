using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CarModelController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarModelController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModel>>> Get()
        {
            var carModels = await _serviceManagerMaster.CarModelService.GetAllAsync(false);
            return Ok(carModels);
        }

        // GET api/<CarModelController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> Get(int id)
        {
            var carModel = await _serviceManagerMaster.CarModelService.GetByIdAsync(id, false);
            return Ok(carModel);
        }

        // POST api/<CarModelController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarModelResponse request)
        {
            if (request == null)
            {
                return BadRequest("Car Brand Request is NOT valid");
            }
            var carModel = await _serviceManagerMaster.CarModelService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = carModel.CarmId }, carModel);
        }

        // PUT api/<CarModelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarModelResponse request)
        {
            await _serviceManagerMaster.CarModelService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE api/<CarModelController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.CarModelService.DeleteAsync(id);

            return NoContent();
        }
    }
}