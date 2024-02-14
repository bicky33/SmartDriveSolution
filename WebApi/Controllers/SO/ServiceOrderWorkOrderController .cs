using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;
using System.Formats.Asn1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderWorkorderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ServiceOrderWorkorderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceControlle>
        [HttpGet]
        public async Task<IActionResult> GetServiceOrderTasks()
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderWorkorderService.GetAllAsync(true);
            return Ok(serviceOrderDto);
        }

        // GET api/<ServiceControlle>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderWorkorderById(int id)
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderWorkorderService.GetByIdAsync(id, false);
            return Ok(serviceOrderDto);
        }

        // POST api/<ServiceControlle>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceOrderWorkorderDtoCreate serviceOrderDto)
        {
            if (serviceOrderDto == null)
                return BadRequest("Service Order Workorder object is not valid");

            var newServiceOrderDto = await _serviceManager.ServiceOrderWorkorderService.CreateAsync(serviceOrderDto);

            return CreatedAtAction(nameof(GetServiceOrderWorkorderById), new { id = newServiceOrderDto.SowoId }, newServiceOrderDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceOrderWorkorderDtoCreate serviceOrderDto)
        {
            var serviceOrder = await _serviceManager.ServiceOrderWorkorderService.GetByIdAsync(id, true);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderWorkorderService.UpdateAsync(id, serviceOrderDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceOrder = await _serviceManager.ServiceOrderWorkorderService.GetByIdAsync(id, false);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderWorkorderService.DeleteAsync(id);
            return Ok();
        }
    }
}
