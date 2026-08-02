using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),

                new Claim(ClaimTypes.Name, user.FullName),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "Customer")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credential = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expire =
                DateTime.Now.AddMinutes(
                    Convert.ToDouble(configuration["Jwt:ExpireMinutes"]));

            var token = new JwtSecurityToken(

                issuer: configuration["Jwt:Issuer"],

                audience: configuration["Jwt:Audience"],

                claims: claims,

                expires: expire,

                signingCredentials: credential

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}