using Contract.DTO.Payment;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Payment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IServicePaymentManager _serviceManager;

        public UserAccountsController(IServicePaymentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UserAccountsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetAll()
        {
            var userAccount = await _serviceManager.UserAccountService.GetAllAsync(false);
            return Ok(userAccount);
        }

        // GET api/<UserAccountsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDto>> GetById(int id)
        {
            var userAccount = await _serviceManager.UserAccountService.GetByIdAsync(id, false);
            return Ok(userAccount);
        }

        // POST api/<UserAccountsController>
        [HttpPost]
        public async Task<ActionResult<UserAccountDto>> Post([FromBody] UserAccountDto dto)
        {
            if (dto == null)
                return BadRequest("Bank object is not valid");
            await _serviceManager.UserAccountService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = dto.UsacId }, dto);
        }

        // PUT api/<UserAccountsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserAccountDto dto)
        {
            await _serviceManager.UserAccountService.UpdateAsync(id, dto);

            return Ok(dto);
        }

        // DELETE api/<UserAccountsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _serviceManager.UserAccountService.DeleteAsync(id);
            return Ok($"ID: {id} has been deleted");
        }
    }
}
