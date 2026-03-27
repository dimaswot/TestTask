using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiresMinutes;

        public JwtTokenService(IConfiguration config)
        {
            _secretKey = config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured");
            _issuer = config["Jwt:Issuer"] ?? "TestTaskAuth";
            _audience = config["Jwt:Audience"] ?? "TestTaskApi";
            _expiresMinutes = config.GetValue<int>("Jwt:ExpiresMinutes", 60);
        }

        public string GenerateToken(int userId, string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiresMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
    
}
