using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;
using System.Formats.Asn1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderTaskController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ServiceOrderTaskController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceControlle>
        [HttpGet]
        public async Task<IActionResult> GetServiceOrderTasks()
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderTaskService.GetAllAsync(true);
            return Ok(serviceOrderDto);
        }

        // GET api/<ServiceControlle>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderTaskById(int id)
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderTaskService.GetByIdAsync(id, false);
            return Ok(serviceOrderDto);
        }
        [HttpGet("Sero/{seroId}")]
        public async Task<IActionResult> GetSeotBySeroId(string seroId)
        {
            var seot = await _serviceManager.ServiceOrderTaskService.GetAllByRelation("SeroId", seroId, false);
            return Ok(seot);
        }


        // POST api/<ServiceControlle>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceOrderTaskDtoCreate serviceOrderDto)
        {
            if (serviceOrderDto == null)
                return BadRequest("Service Order Task object is not valid");

            var newServiceOrderDto = await _serviceManager.ServiceOrderTaskService.CreateAsync(serviceOrderDto);

            return CreatedAtAction(nameof(GetServiceOrderTaskById), new { id = newServiceOrderDto.SeotId }, newServiceOrderDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceOrderTaskDtoCreate serviceOrderDto)
        {
            var serviceOrder = await _serviceManager.ServiceOrderTaskService.GetByIdAsync(id, true);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderTaskService.UpdateAsync(id, serviceOrderDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceOrder = await _serviceManager.ServiceOrderTaskService.GetByIdAsync(id, false);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderTaskService.DeleteAsync(id);
            return Ok();
        }
    }
}
