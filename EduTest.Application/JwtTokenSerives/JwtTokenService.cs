using EduTest.Domain.Models.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.JwtTokenSerives
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenService(IConfiguration configuration)
        {
            _jwtSettings = new JwtSettings();
            ConfigurationBinder.Bind(configuration.GetSection("JwtSettings"), _jwtSettings);
        }




        public string GenerateToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtSettings.ExpiresInHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
