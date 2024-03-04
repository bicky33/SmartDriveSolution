using Contract.DTO.UserModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManagerUser _serviceManager;

        public RoleController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _serviceManager.RoleService.GetAllAsync(false);

            return Ok(roles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{roleName}")]
        public async Task<ActionResult> Get(string roleName)
        {
            var role = await _serviceManager.RoleService.GetByRoleNameAsync(roleName, false);

            return Ok(role);
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleDto body)
        {
            if (body == null)
            {
                return BadRequest();
            }

            var create = await _serviceManager.RoleService.CreateAsync(body);

            return Ok(create);
        }

        // PUT api/<RoleController>/5
        [HttpPut("{roleName}")]
        public async Task<IActionResult> Put(string roleName, [FromBody] RoleDto body)
        {

            await _serviceManager.RoleService.UpdateAsync(roleName, body);

            return Ok(body);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{roleName}")]
        public async Task<IActionResult> Delete(string roleName)
        {
            await _serviceManager.RoleService.DeleteAsync(roleName);

            return NoContent();
        }
    }
}
