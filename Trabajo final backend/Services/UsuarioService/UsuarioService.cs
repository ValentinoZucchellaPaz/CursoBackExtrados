using Data_Access.DAOAuth;
using Models.DTO;
using Models.Entidades;
using Services.Security;
using System.Formats.Asn1;

namespace Services.UsuarioService
{
    public class UsuarioService(IDAOUsuario dao): IUsuarioService
    {
        private readonly IDAOUsuario _dao = dao;

        public async Task<Usuario> Authenticate(DTOLogin request)
        {
            var user = await _dao.GetUsuarioPorMailAsync(request.Email);
            if (user == null || !PasswordHasher.VerifyPassword(request.Contraseña, user.Contraseña, user.Salt))
            {
                throw new Exception($"No se encontro el mail {request.Email} en la base de datos");
            }
            return user;

        }

        public async Task<int?> CrearUsuario(DTOSignUp request)
        {
            // validaciones de role
            if (!Enum.TryParse<UserRole>(request.Role, true, out var parsedRole) || !Enum.IsDefined(typeof(UserRole), parsedRole))
            {
                throw new ArgumentException($"Rol inválido: {request.Role}, los roles son: {String.Join(" | ", Enum.GetNames(typeof(UserRole)))}");
            }
            // validar que solo un admin puede crear organizadores
            // validar que solo un admin puede crear otro admin
            // validar que solo un organizador puede crear jueces
            // hashear pass
            (string pass, string salt) = PasswordHasher.HashPassword(request.Contraseña);
            //subir a db
            var res = await _dao.CrearUsuarioAsync(request.Nombre, request.Pais, request.Email, pass, salt, request.Role, request.Id_creador, request.Alias, request.Avatar);
            // devolver id con la que se subio
            return res;
        }
    }
}
