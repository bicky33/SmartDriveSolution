using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;
using System.Formats.Asn1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePremiCreditController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ServicePremiCreditController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IActionResult> GetSemis()
        {
            var secr = await _serviceManager.ServicePremiCreditService.GetAllAsync(false);
            return Ok(secr);
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSemiById(int id)
        {
            var secr = await _serviceManager.ServicePremiCreditService.GetByIdAsync(id, false);
            return Ok(secr);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServicePremiCreditDtoCreate semiDto)
        {
            if (semiDto == null)
                return BadRequest("Service object is not valid");

            var newSemieDto = await _serviceManager.ServicePremiCreditService.CreateAsync(semiDto);

            return CreatedAtAction(nameof(GetSemiById), new { id = newSemieDto.SecrId }, newSemieDto);
        }

        // PUT api/<ServiceControlle>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServicePremiCreditDtoCreate semiDto)
        {
            var semi = await _serviceManager.ServicePremiCreditService.GetByIdAsync(id, true);
            if (semi == null)
                return NotFound();
            await _serviceManager.ServicePremiCreditService.UpdateAsync(id, semiDto);
            return NoContent();
        }

        // DELETE api/<ServiceControlle>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var semi = await _serviceManager.ServicePremiCreditService.GetByIdAsync(id, false);
            if (semi == null)
                return NotFound();
            await _serviceManager.ServicePremiCreditService.DeleteAsync(id);
            return Ok();
        }
    }
}
