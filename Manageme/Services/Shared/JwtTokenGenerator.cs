using System;
using Manageme.Data.Models;
using ClaimTypes = Manageme.Extensions.ClaimTypes;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Manageme.Services.Shared
{
    public static class JwtTokenGenerator
    {
        public static string GetTokenString(User user, TimeSpan duration, byte[] key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserId, user.Id.ToString()), 
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow + duration
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    } 
}
