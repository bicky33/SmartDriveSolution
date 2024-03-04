using Contract.DTO.UserModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IServiceManagerUser _serviceManager;

        public UserAddressController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UserAddressController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAddressDto>>> Get()
        {
            var userAddresss = await _serviceManager.UserAddressService.GetAllAsync(false);

            return Ok(userAddresss);
        }

        // GET api/<UserAddressController>GetAllById/5
        [HttpGet("GetAllById/{id}")]
        public async Task<ActionResult<IEnumerable<UserAddressDto>>> GetAllById(int id)
        {
            var userAddress = await _serviceManager.UserAddressService.GetAllByIdAsync(id, false);

            return Ok(userAddress);
        }

        // GET api/<UserAddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var userAddress = await _serviceManager.UserAddressService.GetByIdAsync(id, false);

            return Ok(userAddress);
        }

        // POST api/<UserAddressController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAddressDto body)
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);
            body.UsdrEntityid = int.Parse(me.Sub);

            if (body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.UserAddressService.CreateAsync(body);

            return Ok(create);
        }

        // PUT api/<UserAddressController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserAddressDto body)
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);

            var userAddress = await _serviceManager.UserAddressService.GetByIdAsync(id, false);

            if (me.Sub != userAddress.UsdrEntityid.ToString()) return Forbid();

            await _serviceManager.UserAddressService.UpdateAsync(id, body);

            return Ok(body);
        }

        // DELETE api/<UserAddressController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);

            var userAddress = await _serviceManager.UserAddressService.GetByIdAsync(id, false);

            if (me.Sub != userAddress.UsdrEntityid.ToString()) return Forbid();

            await _serviceManager.UserAddressService.DeleteAsync(id);

            return NoContent();
        }
    }
}
