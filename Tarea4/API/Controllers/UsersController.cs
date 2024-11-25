using DAO_Entidades;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly DAOUser _db = DAOUser.GetInstance();

        [HttpGet("search/all")]
        public List<User> GetUsers()
        {
            return _db.GetAllUsers();
        }

        [HttpGet("search/{id}")]
        public User? GetUser(int id)
        {
            return _db.GetUser(id);
        }

        [HttpPost("create")]
        public int CreateUser(MCreateUser request)
        {
            //desestructurar request
            (string name, int age, string mail) = (request.Name, request.Age, request.Mail);
            //validar edad y mail
            if (age < 14 || !mail.Contains("@gmail.com"))
            {
                Console.WriteLine("No se pueden agregar usuarios con edad menor a 14 años ni con mail distinto de @gmail.com");
                return 0;
            }
            //crear usuario
            int id = _db.CreateUser(name, age, mail);
            if (id < 0)
            {
                Console.WriteLine("No se ha podido crear el usuario");
            }
            Console.WriteLine($"El usuario ha sido creado con el id: {id}");
            return id;
        }

        [HttpPost("delete")]
        public bool DeleteUser(MId request)
        {
            int id = request.Id;
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

        [HttpPost("update")]
        public bool UpdateUser(MUpdateUser request)
        {
            //desestructurar request
            (int id, string name, int age, string mail) = (request.Id, request.Name, request.Age, request.Mail);
            //validar edad y mail
            if (age < 14 || !mail.Contains("@gmail.com"))
            {
                Console.WriteLine("No se pueden agregar usuarios con edad menor a 14 años ni con mail distinto de @gmail.com");
                return false;
            }
            //actualizar usuario
            bool res =  _db.UpdateUser(id, name, age, mail);
            if (!res)
            {
                Console.WriteLine($"No se ha encontrado ningun usuario activo con id {id}");
                return res;
            }
            Console.WriteLine($"El usuario con id {id} se ha actualizado correctamente con los valores" +
                $"\nnombre: {name}\nedad: {age}\nmail: {mail}");
            return res;
        }
    }
}
