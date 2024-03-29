﻿using Domain.Entities.Users;
using Domain.Enum;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Authentication
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateJwt(User user)
        {
            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.UserEntityid.ToString()),
                new(JwtRegisteredClaimNames.Email, user.UserEmail)
            };

            claims.Add(new(CustomClaims.Username, user.UserName));

            claims.Add(new(CustomClaims.Image, user.UserPhoto != null ? user.UserPhoto : ""));

            //claim roles
            foreach (var item in user.UserRoles)
            {
                if (item.UsroStatus == EnumRoleActiveStatus.ACTIVE)
                    claims.Add(new Claim(ClaimTypes.Role, item.UsroRoleName));
            }

            var securityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationTimesInMinutes)
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
