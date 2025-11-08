using Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config) => _config = config;

        public string GenerateToken(object user, string role)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Role, role)
        };

            // Uso de Reflection para obtener el ID y Email
            var userId = user.GetType().GetProperty("Id")?.GetValue(user)?.ToString();
            var userEmail = user.GetType().GetProperty("Email")?.GetValue(user)?.ToString();

            if (userId != null) claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            if (userEmail != null) claims.Add(new Claim(ClaimTypes.Email, userEmail));

            var secretKey = _config["JwtSettings:SecretKey"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token válido por 1 hora
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
