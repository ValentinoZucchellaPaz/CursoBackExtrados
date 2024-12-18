namespace DAO_Entidades.DTOs.Token
{
    public class DTOTokens(string accessToken, string refreshToken, DateTime accessTokenExpiration, DateTime refreshTokenExpiration)
    {
        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
        public DateTime AccessTokenExpiration { get; set; } = accessTokenExpiration;
        public DateTime RefreshTokenExpiration { get; set; } = refreshTokenExpiration;
    }
}
