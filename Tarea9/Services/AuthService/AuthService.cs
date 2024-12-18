using Configuration;
using DAO_Entidades.DAO.DAORefreshToken;
using DAO_Entidades.DTOs.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.AuthService
{
    public class AuthService(IDAORefreshToken dao ,IOptions<JwtConfig> options) : IAuthService
    {
        private readonly IDAORefreshToken _db = dao;
        private readonly string _jwtKey = options.Value.Secret;
        private readonly string _jwtIssuer = options.Value.Issuer;
        private readonly int _accessTokenExpiration = 5;

        public async Task<DTOTokens> GenerateTokens(int userId, string userMail, string role)
        {
            // access token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userId.ToString()),
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
            string refreshToken = Guid.NewGuid().ToString();
            DateTime refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // guardar refresh token (hasheado) en db
            var res = await _db.SaveRefreshToken(userId, HashRefreshToken(refreshToken), refreshTokenExpiration);

            if (!res) throw new Exception("no se pudo guardar el refresh token");

            return new DTOTokens(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
        }

        public async Task<bool> ValidateRefreshToken(int userId, string token)
        {
            var refreshToken = await _db.GetRefreshToken(userId, HashRefreshToken(token));

            return refreshToken != null &&
                   refreshToken.ExpirationDate > DateTime.UtcNow &&
                   !refreshToken.IsRevoked;
        }

        public async Task<bool> DeleteRefreshToken(int userId, string token)
        {
            return await _db.DeleteRefreshToken(userId, HashRefreshToken(token));
        }
        private string HashRefreshToken(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
