namespace DAO_Entidades.DTOs.User
{
    public class DTOLogin(string mail, string password)
    {
        public string Mail { get; set; } = mail;
        public string Password { get; set; } = password;
    }
}
