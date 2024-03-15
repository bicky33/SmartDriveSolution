using Contract.DTO.UserModule;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {

        private readonly IServiceManagerUser _serviceManager;

        public UserRolesController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<UserRolesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> Get()
        {
            var userRoles = await _serviceManager.UserRoleService.GetAllAsync(false);

            return Ok(userRoles);
        }

        // GET api/<UserRolesController>GetAllById/5
        [HttpGet("GetAllById/{id}")]
        public async Task<IActionResult> GetAllById(int id)
        {
            var userRoles = await _serviceManager.UserRoleService.GetAllByIdAsync(id, false);

            return Ok(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRoleDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.UserRoleService.CreateAsync(body);

            return Ok(create);
        }

        // DELETE api/<UserRolesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] UserRoleDeleteDto body)
        {
            await _serviceManager.UserRoleService.DeleteAsync(id, body.UsroRoleName);

            return NoContent();
        }

        // DELETE api/<UserRolesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UserRoleDeleteDto body)
        {
            await _serviceManager.UserRoleService.UpdateStatusAsync(id, body.UsroRoleName);

            return Ok(new
            {
                Message = "Update role status success"
            });
        }
    }
}
