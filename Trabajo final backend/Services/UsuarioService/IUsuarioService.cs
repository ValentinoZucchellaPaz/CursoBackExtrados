using Models.DTO;
using Models.Entidades;

namespace Services.UsuarioService
{
    public interface IUsuarioService
    {
        public Task<int?> CrearUsuario(DTOSignUp request);
        public Task<Usuario> Authenticate(DTOLogin request);
    }
}
