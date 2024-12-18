using DAO_Entidades.DTOs.Token;

namespace Services.AuthService
{
    public interface IAuthService
    {
        public Task<DTOTokens> GenerateTokens(int userId, string userMail, string role);
        public Task<bool> ValidateRefreshToken(int userId, string refreshToken);
        public Task<bool> DeleteRefreshToken(int userId, string refreshToken);
    }
}
