using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class AreaWorkgroupController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public AreaWorkgroupController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaWorkgroup>>> Get()
        {
            var areaWorkgroups = await _serviceManagerMaster.AreaWorkgroupService.GetAllAsyncMaster(false);
            return Ok(areaWorkgroups);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<AreaWorkgroup>> Get(string name)
        {
            var areaWorkgroup = await _serviceManagerMaster.AreaWorkgroupService.GetByNameAsyncMaster(name, false);
            return Ok(areaWorkgroup);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AreaWorkgroupResponse request)
        {
            if (request == null)
            {
                return BadRequest("AreaWorkgroup Request is NOT valid");
            }
            var areaWorkgroup = await _serviceManagerMaster.AreaWorkgroupService.CreateAsyncMaster(request);
            return CreatedAtAction(nameof(Get), new { name = areaWorkgroup.ArwgCode}, areaWorkgroup);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] AreaWorkgroupResponse request)
        {
            await _serviceManagerMaster.AreaWorkgroupService.UpdateAsyncMaster(name, request);
            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteInsuranceType(string name)
        {
            await _serviceManagerMaster.AreaWorkgroupService.DeleteAsyncMaster(name);

            return NoContent();
        }
    }
}