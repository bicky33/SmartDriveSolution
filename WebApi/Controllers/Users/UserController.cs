using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.RequestFeatured;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Users
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
        //[Authorize(Roles = "EM")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _serviceManager.UserService.GetAllAsync(false);

            return Ok(users);
        }

        //get all users with pagination
        [Authorize]
        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchUsers(
            [FromQuery] EntityParameter entityParameter
        )
        {
            var users = await _serviceManager.UserService.GetUsersPaging(entityParameter, false);

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
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.UserService.CreateAsync(body);

            return Ok(create);
        }

        // POST api/<UserController>/CreateUserWithRole
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserWithRole([FromBody] UserWithRoleCreateDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var userDto = body.Adapt<UserDto>();
            //role and active from body
            var create = await _serviceManager.UserService.CreateUserWithRole(
                userDto,
                body.RoleName,
                body.IsRoleActive ? EnumRoleActiveStatus.ACTIVE : EnumRoleActiveStatus.INACTIVE
                );

            return Ok(create);
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto body)
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);
            if (me.Sub != id.ToString())
            {
                return Forbid();
            }

            await _serviceManager.UserService.UpdateAsync(id, body);

            return Ok(body);
        }

        // DELETE api/<UserController>/5
        [Authorize(Roles = "EM")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.UserService.DeleteAsync(id);

            return NoContent();
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPatch("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] UserEditProfileRequestDto body)
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);
            if (me.Sub != id.ToString()) return Forbid();

            await _serviceManager.UserService.UpdatePhoto(id, body);

            return Ok(body);
        }

        [Authorize]
        [HttpPut("UpdatePassword/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UserUpdatePasswordRequestDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);
            if (me.Sub != id.ToString()) return Forbid();

            await _serviceManager.UserService.UpdatePassword(id, body);

            return Ok( new {
                Messsage = "Update password success",
            });
        }

        [Authorize]
        [HttpPut("UpdateEmail/{id}")]
        public async Task<IActionResult> UpdateEmail(int id, [FromBody] UserUpdateEmailRequestDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);
            if (me.Sub != id.ToString()) return Forbid();

            await _serviceManager.UserService.UpdateEmail(id, body.UserEmail);

            return Ok(body);
        }

        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            await _serviceManager.UserService.ForgotPassword(body);

            return Ok(new
            {
                Messsage = "Update password success",
            });
        }
    }
}
