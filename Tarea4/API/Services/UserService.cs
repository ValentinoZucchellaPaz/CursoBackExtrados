using DAO_Entidades;

namespace API.Services
{
    public class UserService
    {
        private readonly DAOUser _db;

        public UserService()
        {
            _db = DAOUser.GetInstance();
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
            (string name, int age, string mail) = (user.Name, user.Age, user.Mail);
            //validar edad y mail
            if (age < 14 || !mail.Contains("@gmail.com"))
            {
                Console.WriteLine("No se pueden agregar usuarios con edad menor a 14 años ni con mail distinto de @gmail.com");
                return 0;
            }
            //crear usuario - imprimir en consola resultado
            int id = _db.CreateUser(name, age, mail);
            if (id < 0)
            {
                Console.WriteLine("No se ha podido crear el usuario");
            }
            else
            {
                Console.WriteLine($"El usuario ha sido creado con el id: {id}");
            }
            return id;
        }

        public bool UpdateUser(MUpdateUser user)
        {
            //desestructurar request
            (int id, string name, int age, string mail) = (user.Id, user.Name, user.Age, user.Mail);
            //validar edad y mail
            if (age < 14 || !mail.Contains("@gmail.com"))
            {
                Console.WriteLine("No se pueden agregar usuarios con edad menor a 14 años ni con mail distinto de @gmail.com");
                return false;
            }
            //actualizar usuario
            int res = _db.UpdateUser(id, name, age, mail);
            if (res == 0)
            {
                Console.WriteLine($"No se ha encontrado ningun usuario activo con id {id}");
            }
            else
            {
                Console.WriteLine($"El usuario con id {id} se ha actualizado correctamente con los valores" +
                    $"\nnombre: {name}\nedad: {age}\nmail: {mail}");
            }
            return res > 0;
        }

        public bool DeleteUser(MId user_id)
        {
            int id = user_id.Id;
            //crear fecha de eliminacion de usuario
            DateTime now = DateTime.Now;
            string format = now.ToString("dd MMM yyyy HH:mm").ToUpper();
            //eliminacion de usuario
            bool res = _db.DeleteUser(id, format);
            if (!res)
            {
                Console.WriteLine($"No se ha encontrado ningun usuario activo con id {id}");
                return res;
            }
            Console.WriteLine($"El usuario con id {id} se ha dado de baja en la fecha {format}");
            return res;
        }

    }
}
