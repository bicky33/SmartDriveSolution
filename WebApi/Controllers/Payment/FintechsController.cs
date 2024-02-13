using Contract.DTO.Payment;
using Domain.Entities.Master;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Payment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class FintechsController : ControllerBase
    {
        private readonly IServicePaymentManager _serviceManager;

        public FintechsController(IServicePaymentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        // GET: api/<FintechsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fintech>>> GetFintechs()
        {
            var banks = await _serviceManager.FintechService.GetAllAsync(false);
            return Ok(banks);
        }

        // GET api/<FintechsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FintechDto>> GetFintechById(int id)
        {
            var bank = await _serviceManager.FintechService.GetByIdAsync(id, false);
            return Ok(bank);
        }

        // POST api/<FintechsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FintechDto fintechDto)
        {
            if (fintechDto == null)
                return BadRequest("Bank object is not valid");

            await _serviceManager.FintechService.CreateAsync(fintechDto);
            return CreatedAtAction(nameof(GetFintechById), new { id = fintechDto.FintEntityid }, fintechDto);

        }

        // PUT api/<FintechsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FintechDto fintechDto)
        {
            await _serviceManager.FintechService.UpdateAsync(id, fintechDto);
            return Ok(fintechDto);

        }

        // DELETE api/<FintechsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.FintechService.DeleteAsync(id);
            return Ok($"ID {id} Succesfully deleted");

        }
    }
}
