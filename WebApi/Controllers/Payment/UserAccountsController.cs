using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Enum;
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
        public async Task<ActionResult<UserAccountDto>> Post(AccountTypeEnum accountType, [FromBody] UserAccountCreateDto dto)
        {
            var existData = await _serviceManager.UserAccountService.GetByAccountNoAsync(dto.UsacAccountno, false, ReturnException.RETURN_WHEN_EXIST);
            if (existData != null)
                return BadRequest("Account Number Already Exsist");

            if (dto == null)
                return BadRequest("Bank object is not valid"); 
            var a = await _serviceManager.UserAccountService.CreateAsync(accountType, dto); 
            return CreatedAtAction(nameof(GetById), new { id = a.UsacId }, a);
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
