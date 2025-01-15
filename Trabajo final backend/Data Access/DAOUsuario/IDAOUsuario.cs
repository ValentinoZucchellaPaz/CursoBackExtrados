using Models.DTO;
using Models.Entidades;

namespace Data_Access.DAOAuth
{
    public interface IDAOUsuario
    {
        public Task<int?> CrearUsuarioAsync(string nombre, string pais, string email, string contraseña, string salt, string role, int? id_creador, string? alias, string? avatar);
        public Task<Usuario?> GetUsuarioPorMailAsync(string email);
        //public Task<IEnumerable<Usuario>> GetUsuariosAsync();
        //public Task<IEnumerable<Usuario>> GetAdminsAsync();
        //public Task<IEnumerable<Usuario>> GetJugadoresAsync();
        //public Task<IEnumerable<Usuario>> GetJuecesAsync();
        //public Task<IEnumerable<Usuario>> GetOrganizadoresAsync();

        //public Task<Usuario?> ActualizarUsuario();
        //public Task<bool> BorrarUsuario();
        //public Task<bool> AliasEnUso(string alias);

    }
}
