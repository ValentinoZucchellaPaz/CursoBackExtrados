using DAO_Entidades.DAO.DAOUser;
using DAO_Entidades.Entities;
using DAO_Entidades.Models;
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

        public async Task<User?> Authenticate(string mail, string password)
        {
            var user = await _db.GetUserByMail(mail);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.GetPass(), user.GetSalt()))
            {
                return null;
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _db.GetAllUsers();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _db.GetUser(id);
        }

        public async Task<int> CreateUser(MCreateUser user)
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

        public async Task<int> UpdateUser(MUpdateUser user)
        {
            var userId = GetUserIdFromClaims();

            //validar edad
            if (user.Age < 14)
            {
                throw new UserAgeException("Solo se puede actualizar la edad a más de 14 años");
            }

            //actualizar usuario y retornar num col actualizadas en db
            var res = await _db.UpdateUser(userId, user.Name, user.Age);
            return res == 0 ? res : userId;
        }

        public async Task<bool> DeleteUser(MId userId)
        {
            int id = userId.Id;

            //crear fecha de eliminacion de usuario
            DateTime utcNow = DateTime.UtcNow;

            //eliminar usuario, si hubo exito retornar el id de usuario, sino 0
            return await _db.DeleteUser(id, utcNow);
        }

        private int GetUserIdFromClaims()
        {
            // Obtener el ID del usuario desde los claims del token
            var userIdClaim = (_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid))
                ?? throw new UnauthorizedAccessException("El token no contiene información de usuario.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new InvalidOperationException("El ID del usuario en los claims no es válido.");
            }

            return userId;
        }

    }
}
