namespace API.Services.AuthService
{
    public interface IAuthService
    {
        public string GenerateJwtToken(string userId, string userMail, string role);
    }
}
