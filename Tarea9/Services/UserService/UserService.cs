using DAO_Entidades.DAO.DAOUser;
using DAO_Entidades.DTOs.User;
using DAO_Entidades.Entities;
using Microsoft.AspNetCore.Http;
using Services.Security;
using Services.Security.Exceptions;
using System.Security.Claims;

namespace Services.UserService
{
    public class UserService(IDAOUser db, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IDAOUser _db = db;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<DTOUser?> Authenticate(string mail, string password)
        {
            var user = await _db.GetUserByMail(mail);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.Password, user.Salt))
            {
                return null;
            }
            return new DTOUser(user.Id, user.Name, user.Age, user.Mail, user.UnsubDate, user.Role);
        }

        public async Task<List<DTOUser>> GetUsers()
        {
            var users = await _db.GetAllUsers();
            var DtoUsersList = new List<DTOUser>();
            foreach (User user in users)
            {
                DtoUsersList.Add(new DTOUser(user.Id, user.Name, user.Age, user.Mail, user.UnsubDate, user.Role));
            }
            return DtoUsersList;
        }

        public async Task<DTOUser?> GetUser(int id)
        {
            var user = await _db.GetUser(id);
            return user != null ? new DTOUser(user.Id, user.Name, user.Age, user.Mail, user.UnsubDate, user.Role) : null;
        }

        public async Task<int> CreateUser(DTOCreateUser user)
        {
            //desestructurar request
            (string name, int age, string mail, string password, string role) = (user.Name, user.Age, user.Mail, user.Password, user.Role);
            //validar edad, mail y rol
            if (age < 14)
            {
                throw new UserAgeException("Solo se pueden registrar usuarios mayores a 14 años");
            }
            else if (!mail.Contains("@gmail.com"))
            {
                throw new InvalidMailException("Solo se pueden registar usuarios con mail Gmail (@gmail.com)");
            }
            else if (await _db.IsMailInUse(mail))
            {
                //preguntar si es mejor este error o esperar excepcion de la db
                throw new InvalidMailException("Este correo ya esta siendo usando otro usuario");
            }
            else if (role != "admin" && role != "user")
            {
                //preguntar si es mejor este error o esperar excepcion de la db
                throw new InvalidRoleException("Solo se permiten roles 'admin' o 'user'");
            }

            //crear contraseña hasheada
            var (hash, salt) = PasswordHasher.HashPassword(password);

            //crear usuario y retornar su id de creacion
            return await _db.CreateUser(name, age, mail, hash, salt, role);
        }

        public async Task<int> UpdateUser(DTOUpdateUser user)
        {
            // se actualiza el user dueño del jwt enviado por auth
            var userId = GetUserClaims().Sid;

            //validar edad
            if (user.Age < 14)
            {
                throw new UserAgeException("Solo se puede actualizar la edad a más de 14 años");
            }

            //actualizar usuario y retornar num col actualizadas en db
            var res = await _db.UpdateUser(userId, user.Name, user.Age);
            return res == 0 ? res : userId;
        }

        public async Task<bool> DeleteUser(DTOId userId)
        {
            int id = userId.Id;

            //crear fecha de eliminacion de usuario
            DateTime utcNow = DateTime.UtcNow;

            //eliminar usuario, si hubo exito retornar el id de usuario, sino 0
            return await _db.DeleteUser(id, utcNow);
        }

        // esto va a leer el token pasado por authentication y sacar las claims de este, si no se pasa ningun jwt por auth no lee nada
        public DTOUserClaims GetUserClaims()
        {
            // Extraer las claims del HttpContext
            var sid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid);
            var mail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email);
            var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role);

            if (sid == null || mail == null || role == null)
            {
                throw new UnauthorizedAccessException("Las claims no están disponibles en el token.");
            }

            if (!int.TryParse(sid.Value, out int userId))
            {
                throw new InvalidOperationException("El ID del usuario en los claims no es válido.");
            }

            return new DTOUserClaims(userId, mail.Value, role.Value);
        }

    }
}
