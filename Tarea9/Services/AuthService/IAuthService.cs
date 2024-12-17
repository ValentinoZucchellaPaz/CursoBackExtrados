using DAO_Entidades.Models;

namespace Services.AuthService
{
    public interface IAuthService
    {
        public Task<TokenResponse> GenerateTokens(string userId, string userMail, string role);
        public Task<bool> ValidateRefreshToken(string userId, string refreshToken);
        public Task<bool> RevokeRefreshToken(string userId, string refreshToken);
    }
}
