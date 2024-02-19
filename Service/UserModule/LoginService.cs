using Contract.DTO.UserModule;
using Domain.Authentication;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class LoginService : IServiceLogin
    {
        private readonly IRepositoryManagerUser _repositoryManager;
        private readonly JwtTokenGenerator _jwtGenerator;

        public LoginService(IRepositoryManagerUser repositoryManager, JwtTokenGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _repositoryManager = repositoryManager;
        }

        public LoginClaimsDto GetCurrentUser(ClaimsPrincipal user)
        {
            LoginClaimsDto currentClaim = new LoginClaimsDto()
            {
                Sub = user.FindFirstValue(JwtRegisteredClaimNames.Sub),
                Email = user.FindFirstValue(JwtRegisteredClaimNames.Email),
                Username = user.FindFirstValue(CustomClaims.Username),
                Roles = user.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList()
            };

            return currentClaim;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = await _repositoryManager.UserRepository.GetUserByUsername(loginDto.UserName, false);

            if(user == null)
            {
                throw new EntityBadRequestException("Incorrect Username");
            }

            if(BCrypt.Net.BCrypt.Verify(loginDto.UserPassword, user.UserPassword) == false)
            {
                throw new EntityBadRequestException("Incorrect Password");
            }

            var newAccessToken = _jwtGenerator.GenerateJwt(user);

            var result = new LoginResponseDto
            {
                accessToken = newAccessToken,
            };

            return result;
        }
    }
}
