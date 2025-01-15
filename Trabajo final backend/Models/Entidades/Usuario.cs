namespace Models.Entidades
{
    public class Usuario(int id, string nombre, string pais, string email, string contraseña, string salt, string role, int? id_creador, string? alias, string? avatar)
    {
        public int Id = id;
        public string Nombre = nombre;
        public string Pais = pais;
        public string Email = email;
        public string Contraseña = contraseña;
        public string Salt = salt;
        private string _role = role;
        public string Role
        {
            get => _role;
            init
            {
                if (!Enum.TryParse<UserRole>(value, true, out var parsedRole) || !Enum.IsDefined(typeof(UserRole), parsedRole))
                {
                    throw new ArgumentException($"Rol inválido: {value}");
                }
                _role = value;
            }
        }
        public int? Id_creador = id_creador;
        public string? Alias = alias;
        public string? Avatar = avatar;
    }
}
