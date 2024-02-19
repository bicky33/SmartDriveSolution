using Contract.DTO.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceLogin
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        LoginClaimsDto GetCurrentUser(ClaimsPrincipal user);
    }
}
