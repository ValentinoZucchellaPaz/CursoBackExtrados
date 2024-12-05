using Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.AuthService
{
    public class AuthService(IOptions<JwtConfig> options) : IAuthService
    {
        private readonly string _jwtKey = options.Value.Secret;
        private readonly string _jwtIssuer = options.Value.Issuer;

        public string GenerateJwtToken(string userId, string userMail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Email, userMail)
            };

            var securityToken = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
