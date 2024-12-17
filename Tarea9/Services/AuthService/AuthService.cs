using Configuration;
using DAO_Entidades.DAO.DAOUser;
using DAO_Entidades.Entities;
using DAO_Entidades.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthService
{
    public class AuthService(IDAOUser dao ,IOptions<JwtConfig> options) : IAuthService
    {
        private readonly IDAOUser _db = dao;
        private readonly string _jwtKey = options.Value.Secret;
        private readonly string _jwtIssuer = options.Value.Issuer;
        private readonly int _accessTokenExpiration = 5;

        public async Task<TokenResponse> GenerateTokens(string userId, string userMail, string role)
        {
            // access token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Email, userMail),
                new Claim(ClaimTypes.Role, role)
            };
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_accessTokenExpiration);

            var securityToken = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: accessTokenExpiration,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            // refresh token
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            // guardar refresh token en db



            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiration = accessTokenExpiration
            };
        }

        public async Task<bool> ValidateRefreshToken(string userId, string refreshToken)
        {
            return false;
        }

        public async Task<bool> RevokeRefreshToken(string userId, string refreshToken)
        {
            return false;
        }
    }
}
