using Contract.DTO.UserModule;
using Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.User;
using System.Security.Claims;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManagerUser _serviceManager;

        public AuthController(IServiceManagerUser serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto body)
        {
            var login = await _serviceManager.LoginService.Login(body);

            return Ok(login);
        }

        [HttpGet("Me")]
        [Authorize]
        public IActionResult CurrentUser()
        {
            var me = _serviceManager.LoginService.GetCurrentUser(HttpContext.User);

            return Ok(me);
        }
    }
}
