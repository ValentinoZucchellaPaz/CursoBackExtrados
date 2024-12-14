namespace DAO_Entidades.Models
{
    public class MLogin (string mail, string password)
    {
        public string Mail { get; set; } = mail;
        public string Password { get; set; } = password;
    }
}
