using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InsuranceTypeController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public InsuranceTypeController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceType>>> Get()
        {
            var insuranceTypes = await _serviceManagerMaster.InsuranceTypeService.GetAllAsyncMaster(false);
            return Ok(insuranceTypes);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Category>> Get(string name)
        {
            var insuranceType = await _serviceManagerMaster.InsuranceTypeService.GetByNameAsyncMaster(name, false);
            return Ok(insuranceType);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsuranceTypeResponse request)
        {
            if (request == null)
            {
                return BadRequest("Category Request is NOT valid");
            }
            var insuranceType = await _serviceManagerMaster.InsuranceTypeService.CreateAsyncMaster(request);
            return CreatedAtAction(nameof(Get), new { id = insuranceType.IntyName}, insuranceType);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] InsuranceTypeResponse request)
        {
            await _serviceManagerMaster.InsuranceTypeService.UpdateAsyncMaster(name, request);
            return CreatedAtAction(nameof(Get), new { name = request.IntyName }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _serviceManagerMaster.InsuranceTypeService.DeleteAsyncMaster(name);

            return NoContent();
        }
    }
}