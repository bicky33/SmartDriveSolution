using Contract.DTO.Payment;
using Domain.Entities.Master;
using Domain.Entities.Payment;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Domain.Repositories;
using NuGet.Protocol;
using Service.Abstraction.Payment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IServicePaymentManager _serviceManager;

        public BanksController(IServicePaymentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<BanksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            var banks = await _serviceManager.BankService.GetAllAsync(false);
            return Ok(banks);
        }
        // GET api/<BanksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankDto>> GetBankById(int id)
        {
            var bank = await _serviceManager.BankService.GetByIdAsync(id, false);
            return Ok(bank);
        }

        // POST api/<BanksController>
        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] BankDto bankDto)
        {
            if (bankDto == null)
                return BadRequest("Bank object is not valid");

            await _serviceManager.BankService.CreateAsync(bankDto);
            return CreatedAtAction(nameof(GetBankById), new { id = bankDto.BankEntityid }, bankDto);
        }

        // PUT api/<BanksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BankDto bankDto)
        {
            await _serviceManager.BankService.UpdateAsync(id, bankDto);
            return Ok(bankDto);
        }

        // DELETE api/<BanksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.BankService.DeleteAsync(id);
            return Ok($"ID {id} Succesfully deleted");
        }
    }
}
