using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;
using System.Formats.Asn1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ServiceOrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceControlle>
        [HttpGet]
        public async Task<IActionResult> GetServiceOrders()
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderService.GetAllAsync(false);
            return Ok(serviceOrderDto);
        }

        // GET api/<ServiceControlle>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderById(string id)
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderService.GetByIdAsync(id, false);
            return Ok(serviceOrderDto);
        }

        // POST api/<ServiceControlle>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceOrderDtoCreate serviceOrderDto)
        {
            if (serviceOrderDto == null)
                return BadRequest("Service Order object is not valid");

            var newServiceOrderDto = await _serviceManager.ServiceOrderService.CreateAsync(serviceOrderDto);

            return CreatedAtAction(nameof(GetServiceOrderById), new { id = newServiceOrderDto.SeroId }, newServiceOrderDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ServiceOrderDtoCreate serviceOrderDto)
        {
            var serviceOrder = await _serviceManager.ServiceOrderService.GetByIdAsync(id, true);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderService.UpdateAsync(id, serviceOrderDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var serviceOrder = await _serviceManager.ServiceOrderService.GetByIdAsync(id, false);
            if (serviceOrder == null)
                return NotFound();
            await _serviceManager.ServiceOrderService.DeleteAsync(id);
            return Ok();
        }
    }
}
