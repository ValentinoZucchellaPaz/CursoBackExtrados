using API.Auxiliar;
using API.Auxiliar.Exceptions;
using DAO_Entidades;
using DAO_Entidades.Models;
using MySqlConnector;

namespace API.Services.UserService
{
    public class UserService(IDAOUser db) : IUserService
    {
        private readonly IDAOUser _db = db;

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
            (string name, int age, string mail, string password) = (user.Name, user.Age, user.Mail, user.Password);
            //validar edad y mail
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
                throw new InvalidMailException("Este correo ya esta siendo usando otro usuario");
            }

            //crear contraseña hasheada
            var (hash, salt) = PasswordHasher.HashPassword(password);

            //crear usuario y retornar su id de creacion
            return _db.CreateUser(name, age, mail, hash, salt);
        }

        public int UpdateUser(MUpdateUser user)
        {
            //desestructurar request
            (int id, string name, int age) = (user.Id, user.Name, user.Age);
            //validar edad
            if (age < 14)
            {
                throw new UserAgeException("Solo se puede actualizar la edad a más de 14 años");
            }

            //actualizar usuario y retornar num col actualizadas en db
            return _db.UpdateUser(id, name, age);
        }

        public bool DeleteUser(MId user_id)
        {
            int id = user_id.Id;
            //crear fecha de eliminacion de usuario
            DateTime now = DateTime.Now;
            string format = now.ToString("dd MMM yyyy HH:mm").ToUpper();

            //eliminar usuario y retornar si se elimino o no
            return _db.DeleteUser(id, format);
        }

    }
}
