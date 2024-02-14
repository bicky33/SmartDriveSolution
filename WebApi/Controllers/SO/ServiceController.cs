using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;
using System.Formats.Asn1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var categoryDtos = await _serviceManager.ServiceService.GetAllAsync(false);
            return Ok(categoryDtos);
        }

        // GET api/<ServiceControlle>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var categoryDto = await _serviceManager.ServiceService.GetByIdAsync(id, false);
            return Ok(categoryDto);
        }

        // POST api/<ServiceControlle>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceDtoCreate serviceDto)
        {
            if (serviceDto == null)
                return BadRequest("Category object is not valid");

            var newServiceDto = await _serviceManager.ServiceService.CreateAsync(serviceDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = newServiceDto.ServId }, newServiceDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceDtoCreate serviceDto)
        {
            var service = await _serviceManager.ServiceService.GetByIdAsync(id, true);
            if (service == null)
                return NotFound();
            await _serviceManager.ServiceService.UpdateAsync(id, serviceDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceManager.ServiceService.GetByIdAsync(id, false);
            if (service == null)
                return NotFound();
            await _serviceManager.ServiceService.DeleteAsync(id);
            return Ok();
        }
    }
}
