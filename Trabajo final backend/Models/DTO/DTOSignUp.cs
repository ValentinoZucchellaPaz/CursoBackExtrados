namespace Models.DTO
{
    public class DTOSignUp(string nombre, string pais, string email, string contraseña, string role, int? id_creador=null, string? alias=null, string? avatar=null)
    {
        public string Nombre { get; set; } = nombre;
        public string Pais { get; set; } = pais;
        public string Email { get; set; } = email;
        public string Contraseña { get; set; } = contraseña;
        public string Role { get; set; } = role;
        public int? Id_creador { get; set; } = id_creador;
        public string? Alias { get; set; } = alias;
        public string? Avatar { get; set; } = avatar;
    }
}
