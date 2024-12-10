using API.Auxiliar;
using API.Auxiliar.Exceptions;
using DAO_Entidades;
using DAO_Entidades.Models;
using MySqlConnector;
using System.Security.Claims;

namespace API.Services.UserService
{
    public class UserService(IDAOUser db, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IDAOUser _db = db;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public User? Authenticate(string mail, string password)
        {
            var user = _db.GetUserByMail(mail);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.GetPass(), user.GetSalt()))
            {
                return null;
            }
            return user;
        }

        public List<User> GetUsers()
        {
            return _db.GetAllUsers();
        }

        public User? GetUser(int id)
        {
            return _db.GetUser(id);
        }

        public int CreateUser(MCreateUser user)
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
            else if (_db.IsMailInUse(mail))
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
            return _db.CreateUser(name, age, mail, hash, salt, role);
        }

        public int UpdateUser(MUpdateUser user)
        {
            var userId = GetUserIdFromClaims();

            //validar edad
            if (user.Age < 14)
            {
                throw new UserAgeException("Solo se puede actualizar la edad a más de 14 años");
            }

            //actualizar usuario y retornar num col actualizadas en db
            var res = _db.UpdateUser(userId, user.Name, user.Age);
            return res == 0 ? res : userId;
        }

        public bool DeleteUser(MId userId)
        {
            int id = userId.Id;

            //crear fecha de eliminacion de usuario
            DateTime utcNow = DateTime.UtcNow;

            //eliminar usuario, si hubo exito retornar el id de usuario, sino 0
            return _db.DeleteUser(id, utcNow);
        }

        public List<Book> GetBooks()
        {
            return _db.GetBooks();
        }

        public bool RentBook(string bookName)
        {
            var userId = GetUserIdFromClaims();

            // Verificar que no este alquilado en este momento el libro
            DateTime utcNow = DateTime.UtcNow;
            var bookToRent = _db.GetBook(bookName) ?? throw new RentBookException("Este libro no existe, revise los nombres de libro existentes");
            if(bookToRent.ExpirationDate > utcNow)
            {
                throw new RentBookException($"Este libro esta siendo alquilado ahora mismo por el usuario {bookToRent.userId} hasta la fecha {bookToRent.ExpirationDate}");
            }

            // Alquilar
            DateTime expirationDate = utcNow.AddDays(5);
            var res = _db.RentBook(bookName, utcNow, expirationDate, userId);
            return res;
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
