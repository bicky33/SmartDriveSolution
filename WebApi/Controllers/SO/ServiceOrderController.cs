using Contract.DTO.SO;
using Domain.Entities.Master;
using Domain.Entities.SO;
using Domain.RequestFeatured;
using Domain.RequestFeatured.SO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Service.Abstraction.SO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "EM")]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ServiceOrderController(IServiceSOManager serviceManager)
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
        // GET api/<ServiceControlle>/5
        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetServiceOrderByAgentId(int id)
        {
            var serviceOrderDto = await _serviceManager.ServiceOrderService.GetAllByAgentId(id, false);
            return Ok(serviceOrderDto);
        }

        [HttpGet("paginate")]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetServiceOrderWithPagination([FromQuery] EntityParameterSO entityParameter)
        {
            var serviceOrders = await _serviceManager.ServiceOrderService.GetAllWithPagingAsync(entityParameter, false);

            Response.Headers.Add("X-Total-Pages", $"{serviceOrders.TotalPages}");
            Response.Headers.Add("X-Current-Pages", $"{serviceOrders.CurrentPage}");
            Response.Headers.Add("X-HasNext", $"{serviceOrders.HasNext}");
            Response.Headers.Add("X-HasPrevious", $"{serviceOrders.HasPrevious}");
            Response.Headers.Add("X-Total-Count", $"{serviceOrders.TotalCount}");

            return Ok(serviceOrders);
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
