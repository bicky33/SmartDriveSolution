using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class RegionPlatController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public RegionPlatController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionPlat>>> Get()
        {
            var regionPlat = await _serviceManagerMaster.RegionPlatService.GetAllAsyncMaster(false);
            return Ok(regionPlat);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<RegionPlat>> Get(string name)
        {
            var regionPlat = await _serviceManagerMaster.RegionPlatService.GetByNameAsyncMaster(name, false);
            return Ok(regionPlat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionPlatResponse request)
        {
            if (request == null)
            {
                return BadRequest("RegionPlat Request is NOT valid");
            }
            var regionPlat = await _serviceManagerMaster.RegionPlatService.CreateAsyncMaster(request);
            return CreatedAtAction(nameof(Get), new { name = regionPlat.RegpName }, regionPlat);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] RegionPlatResponse request)
        {
            await _serviceManagerMaster.RegionPlatService.UpdateAsyncMaster(name, request);
            return CreatedAtAction(nameof(Get), new { name = request.RegpName }, request);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _serviceManagerMaster.RegionPlatService.DeleteAsyncMaster(name);

            return NoContent();
        }
    }
}