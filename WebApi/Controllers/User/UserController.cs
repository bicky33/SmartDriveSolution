using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.UserModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IServiceManagerUser _serviceManager;

        public UserController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "EM")]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _serviceManager.UserService.GetAllAsync(false);

            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _serviceManager.UserService.GetByIdAsync(id, false);

            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto body)
        {
            if(body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.UserService.CreateAsync(body);

            return Ok(create);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto body)
        {
            await _serviceManager.UserService.UpdateAsync(id, body);

            return Ok(body);
        }

        // DELETE api/<UserController>/5
        [Authorize(Roles = "PC")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.UserService.DeleteAsync(id);

            return NoContent();
        }

        // PUT api/<UserController>/5
        [HttpPatch("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] UserEditProfileRequestDto body)
        {
            await _serviceManager.UserService.UpdatePhoto(id, body);

            return Ok(body);
        }
    }
}
