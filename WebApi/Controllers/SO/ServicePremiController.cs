using Contract.DTO.SO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "EM")]
    public class ServicePremiController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ServicePremiController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var semiDtos = await _serviceManager.ServicePremiService.GetAllAsync(false);
            return Ok(semiDtos);
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var semiDtos = await _serviceManager.ServicePremiService.GetByIdAsync(id, false);
            return Ok(semiDtos);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServicePremiDtoCreate semiDto)
        {
            if (semiDto == null)
                return BadRequest("Service object is not valid");

            var newSemiDto = await _serviceManager.ServicePremiService.CreateAsync(semiDto);

            return CreatedAtAction(nameof(GetServiceById), new { id = newSemiDto.SemiServId }, newSemiDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServicePremiDtoCreate semiDto)
        {
            var semi = await _serviceManager.ServicePremiService.GetByIdAsync(id, true);
            if (semi == null)
                return NotFound();
            await _serviceManager.ServicePremiService.UpdateAsync(id, semiDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var semi = await _serviceManager.ServicePremiService.GetByIdAsync(id, false);
            if (semi == null)
                return NotFound();
            await _serviceManager.ServicePremiService.DeleteAsync(id);
            return Ok();
        }
    }
}
