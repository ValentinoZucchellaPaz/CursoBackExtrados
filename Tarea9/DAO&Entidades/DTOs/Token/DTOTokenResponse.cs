using DAO_Entidades.DTOs.User;

namespace DAO_Entidades.DTOs.Token
{
    public class DTOTokenResponse(string accessToken, DateTime accessTokenExpiration, DTOUser userData)
    {
        public string AccessToken { get; set; } = accessToken;
        public DateTime AccessTokenExpiration { get; set; } = accessTokenExpiration;
        public DTOUser UserData { get; set; } = userData;
    }
}
