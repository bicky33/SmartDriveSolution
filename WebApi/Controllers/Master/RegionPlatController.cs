using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionPlatController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public RegionPlatController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionPlat>>> GetAllInsuranceType()
        {
            var regionPlat = await _serviceManagerMaster.RegionPlatService.GetAllAsyncMaster(false);
            return Ok(regionPlat);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<RegionPlat>> GetInsuranceTypeByName(string name)
        {
            var regionPlat = await _serviceManagerMaster.RegionPlatService.GetByNameAsyncMaster(name, false);
            return Ok(regionPlat);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateInsuranceType([FromBody] RegionPLatResponse request)
        {
            if (request == null)
            {
                return BadRequest("RegionPlat Request is NOT valid");
            }
            var regionPlat = await _serviceManagerMaster.RegionPlatService.CreateAsyncMaster(request);
            return CreatedAtAction(nameof(GetInsuranceTypeByName), new { name = regionPlat.RegpName}, regionPlat);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateInsuranceType(string name, [FromBody] RegionPLatResponse request)
        {
            await _serviceManagerMaster.RegionPlatService.UpdateAsyncMaster(name, request);
            return CreatedAtAction(nameof(GetInsuranceTypeByName), new { name = request.RegpName }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteInsuranceType(string name)
        {
            await _serviceManagerMaster.RegionPlatService.DeleteAsyncMaster(name);

            return NoContent();
        }
    }
}