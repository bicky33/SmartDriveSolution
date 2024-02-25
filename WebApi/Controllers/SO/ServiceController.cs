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
        private readonly IServiceSOManager _serviceManager;

        public ServiceController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var service = await _serviceManager.ServiceService.GetAllAsync(false);
            return Ok(service);
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceManager.ServiceService.GetByIdAsync(id, false);
            return Ok(service);
        }
        // GET api/<ServiceController>/5
        [HttpGet("search")]
        public async Task<IActionResult> GetServiceBySeroId(string seroid)
        {
            var service = await _serviceManager.ServiceService.SearchBySeroId(seroid);
            return Ok(service);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceDtoCreate serviceDto)
        {
            if (serviceDto == null)
                return BadRequest("Service object is not valid");

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
        // POST api/<ServiceController>
        [HttpPost("CreateServiceFeasibility")]
        public async Task<IActionResult> CreateServiceFeasibility([FromBody] CreateServicePolisFeasibilityDto createServicePolisFeasibilityDto)
        {
            if (createServicePolisFeasibilityDto == null)
                return BadRequest("Service object is not valid");

            var service = await _serviceManager.ServiceService.CreateServiceFeasibility(createServicePolisFeasibilityDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServId }, service);
        }
        // POST api/<ServiceController>
        [HttpPost("CreateServicePolis")]
        public async Task<IActionResult> CreateServicePolis([FromBody] CreateServicePolisDto createServicePolisDto)
        {
            if (createServicePolisDto == null)
                return BadRequest("Service object is not valid");

            var service = await _serviceManager.ServiceService.CreateServicePolis(createServicePolisDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServId }, service);
        }
        // POST api/<ServiceController>
        [HttpPost("ClaimPolis")]
        public async Task<IActionResult> ClaimPolis([FromBody] CreateClaimPolisDto createClaimPolisDto)
        {
            if (createClaimPolisDto == null)
                return BadRequest("Service object is not valid");
            var service = await _serviceManager.ServiceService.CreateClaimPolis(createClaimPolisDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServId }, service);
        }
        // POST api/<ServiceController>
        [HttpPost("ClosePolis")]
        public async Task<IActionResult> ClosePolis([FromBody] int servId, string reason)
        {
            if (reason == null)
                return BadRequest("Reason Required");
            var service = await _serviceManager.ServiceService.ClosePolis(servId,reason);

            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServId }, service);
        }
    }
}
