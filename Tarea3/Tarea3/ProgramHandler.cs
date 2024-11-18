using DAO.Entidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Tarea3
{
    public class ProgramHandler
    {
        private DAOUsuario db;
        public ProgramHandler(DAOUsuario db)
        {
            this.db = db;
        }

        public void CreateUser(string nombre, int edad, string mail)
        {
            try
            {
                int user_id = db.CreateUser(nombre, edad, mail);
                if (user_id == 0)
                {
                    Console.WriteLine($"No se ha encontrado ningun usuario con el id: {user_id}");
                }
                else
                {
                    Console.WriteLine($"El usuario se ha creado exitosamente con id: {user_id}");
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Hubo un error conectandose a la base de datos: {e.Message}");
                Console.WriteLine($"Código de error: {e.ErrorCode}");
            }
        }
        
        public void PrintAllUsers()
        {
            try
            {
                List<Usuario> users = db.GetUsers();
                foreach (Usuario user in users)
                {
                    Console.WriteLine(user);
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine($"Hubo un error conectandose a la base de datos: {e.Message}");
                Console.WriteLine($"Código de error: {e.ErrorCode}");
            }
            
        }

        public void PrintUserById(int id)
        {
            try
            {
                Usuario? user = db.GetUserById(id);
                if (user == null)
                {
                    Console.WriteLine($"No se ha encontrado ningun usuario con el id: {id}");
                }
                else
                {
                    Console.WriteLine(user);
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine($"Hubo un error conectandose a la base de datos: {e.Message}");
                Console.WriteLine($"Código de error: {e.ErrorCode}");
            }
            
        }

        public void DeleteUserById(int id)
        {
            try
            {
                DateTime now = DateTime.Now;
                string format = now.ToString("dd MMM yyyy HH:mm").ToUpper();
                int deleted = db.DeleteUserById(id, format);
                if (deleted == 0)
                {
                    Console.WriteLine($"No se ha encontrado ningun usuario activo con el id: {id}");
                }
                else
                {
                    Console.WriteLine($"El usuario con id: {id} se ha eliminado en la fecha: {format}");
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine($"Hubo un error conectandose a la base de datos: {e.Message}");
                Console.WriteLine($"Código de error: {e.ErrorCode}");
            }
        }

        public void UpdateUserById(int id, string nombre, int edad, string mail)
        {
            try
            {
                int updated = db.UpdateUserById(id, nombre, edad, mail);
                if (updated == 0)
                {
                    Console.WriteLine($"No se ha encontrado ningun usuario con el id: {id}");
                }
                else
                {
                    Console.WriteLine($"El usuario con id: {id} se ha actualizado correctamente.");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Hubo un error conectandose a la base de datos: {e.Message}");
                Console.WriteLine($"Código de error: {e.ErrorCode}");
            }
        }
    }
}
