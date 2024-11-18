using DAO.Entidades;
using Dapper;
using MySqlConnector;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;

namespace DAO
{
    public class DAOUsuario
    {
        private readonly string connectionStr = "Server=localhost;Port=3306;User ID=tarea_3_user;Password=tarea_3_user;Database=tarea3";

        public Usuario? GetUserById(int id)
        {
            string query = "select * from usuarios where id = @id;";
            using (var conn = new MySqlConnection(connectionStr))
            {
                conn.Open();
                var value = conn.QueryFirstOrDefault<Usuario>(query, new { id });
                if(value == null || value.FechaBaja != null)
                {
                    //no se encontro o el usuario se dio de baja
                    return null;
                }
                else
                {
                    return value;
                }
            }
        }

        //devuelve todos los usuarios activos
        public List<Usuario> GetUsers()
        {
            string query = "select * from usuarios where fecha_baja is null";
            using (var conn = new MySqlConnection(connectionStr))
            {
                conn.Open();
                var value = conn.Query<Usuario>(query).ToList();
                return value;
            }
        }

        // retorna el numero de filas afectadas
        public int CreateUser(string nombre, int edad, string mail)
        {
            string query = "insert into usuarios (nombre, edad, mail) values (@nombre, @edad, @mail);";
            using (var conn = new MySqlConnection(connectionStr))
            {
                conn.Open();
                var value = conn.Execute(query, new { nombre, edad, mail });
                if (value != 0)
                {
                    //se ha creado correctamente el usuario
                    int user_id = conn.QuerySingle<int>("select last_insert_id();");
                    return user_id;
                }
                return value;
            }
        }

        // retorna el numero de filas afectadas
        public int DeleteUserById(int id, string fecha_baja)
        {
            //primero reviso si ya se elimino ese user, sino, lo elimino
            string verifier_query = "select fecha_baja from usuarios where id=@id";
            string query = "update usuarios set fecha_baja = @fecha_baja where id=@id;";
            using (var conn = new MySqlConnection(connectionStr))
            {
                conn.Open();
                var fecha_baja_user = conn.QueryFirstOrDefault<string?>(verifier_query, new { id });
                if(fecha_baja_user != null)
                {
                    //si ya se puso una fecha_baja, se devuelve que no se afectaron filas sin siquiera hacer la consulta
                    return 0;
                }
                var afected_rows = conn.Execute(query, new { fecha_baja, id });
                return afected_rows;
            }
        }

        // retorna el numero de filas afectadas
        public int UpdateUserById(int id, string nombre, int edad, string mail)
        {
            //primero reviso si el user esta activo, si no esta activo no lo actualizo
            string verifier_query = "select fecha_baja from usuarios where id=@id";
            string query = "update usuarios set nombre=@nombre, edad=@edad, mail=@mail where id=@id";
            using (var conn = new MySqlConnection(connectionStr))
            {
                conn.Open();
                var fecha_baja_user = conn.QueryFirstOrDefault<string?>(verifier_query, new { id });
                if (fecha_baja_user != null)
                {
                    //si ya se puso una fecha_baja, se devuelve que no se afectaron filas sin siquiera hacer la consulta
                    return 0;
                }
                var value = conn.Execute(query, new { nombre, edad, mail, id});
                return value;
            }
        }

        
    }
}
