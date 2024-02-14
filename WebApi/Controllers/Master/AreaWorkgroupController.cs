using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AreaWorkgroupController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public AreaWorkgroupController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaWorkgroup>>> GetAllInsuranceType()
        {
            var areaWorkgroups = await _serviceManagerMaster.AreaWorkgroupService.GetAllAsyncMaster(false);
            return Ok(areaWorkgroups);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<AreaWorkgroup>> GetInsuranceTypeByName(string name)
        {
            var areaWorkgroup = await _serviceManagerMaster.AreaWorkgroupService.GetByNameAsyncMaster(name, false);
            return Ok(areaWorkgroup);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateInsuranceType([FromBody] AreaWorkgroupResponse request)
        {
            if (request == null)
            {
                return BadRequest("AreaWorkgroup Request is NOT valid");
            }
            var areaWorkgroup = await _serviceManagerMaster.AreaWorkgroupService.CreateAsyncMaster(request);
            return CreatedAtAction(nameof(GetInsuranceTypeByName), new { name = areaWorkgroup.ArwgCode}, areaWorkgroup);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateInsuranceType(string name, [FromBody] AreaWorkgroupResponse request)
        {
            await _serviceManagerMaster.AreaWorkgroupService.UpdateAsyncMaster(name, request);
            return CreatedAtAction(nameof(GetInsuranceTypeByName), new { name = request.ArwgCode }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteInsuranceType(string name)
        {
            await _serviceManagerMaster.AreaWorkgroupService.DeleteAsyncMaster(name);

            return NoContent();
        }
    }
}