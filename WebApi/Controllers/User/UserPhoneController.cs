using Contract.DTO.UserModule;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhoneController : ControllerBase
    {
        private readonly IServiceManagerUser _serviceManager;

        public UserPhoneController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UserPhoneController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPhoneDto>>> Get()
        {
            var userPhones = await _serviceManager.UserPhoneService.GetAllAsync(false);

            return Ok(userPhones);
        }

        // GET api/<UserPhoneController>GetAllById/5
        [HttpGet("GetAllById/{id}")]
        public async Task<IActionResult> GetAllById(int id)
        {
            var userPhone = await _serviceManager.UserPhoneService.GetAllByIdAsync(id, false);

            return Ok(userPhone);
        }

        [HttpGet("GetSingleUserPhone/{id}/{phoneNumber}")]
        public async Task<IActionResult> GetSingleUserPhone(int id, string phoneNumber)
        {
            var userPhone = await _serviceManager.UserPhoneService.GetByIdAndPhoneNumberAsync(id, phoneNumber, false);

            return Ok(userPhone);
        }

        // POST api/<UserPhoneController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPhoneDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.UserPhoneService.CreateAsync(body);

            return Ok(create);
        }

        // PUT api/<UserPhoneController>/5
        [HttpPut("{id}/{phoneNumber}")]
        public async Task<IActionResult> Put(int id, string phoneNumber, [FromBody] UserPhoneDto body)
        {
            await _serviceManager.UserPhoneService.UpdateAsync(id, phoneNumber, body);

            return Ok(body);
        }

        // DELETE api/<UserPhoneController>/5
        [HttpDelete("{id}/{phoneNumber}")]
        public async Task<IActionResult> Delete(int id, string phoneNumber)
        {
            await _serviceManager.UserPhoneService.DeleteAsync(id, phoneNumber);

            return NoContent();
        }
    }
}
