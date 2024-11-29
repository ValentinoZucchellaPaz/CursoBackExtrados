using MySqlConnector;
using Dapper;
using System.Data;
using System.Xml.Linq;
using DAO_Entidades.Models;

namespace DAO_Entidades
{
    public class DAOUser
    {
        //implementacion singleton del DAOUser
        private static DAOUser? _instance = null;

        private DAOUser() { }
        public static DAOUser GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DAOUser();
            }
            return _instance;
        }

        //implementacion del DAO
        private readonly string ConnectionString = "Server=localhost;Port=3306;User ID=tarea_5_user;Password=tarea_5_user;Database=tarea5";

        public List<User> GetAllUsers()
        {
            //trae usuarios activos
            string query = "select * from usuarios where unsub_date is null;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var res = conn.Query<User>(query).ToList();
                return res;
            }
        }

        public User? GetUser(int id)
        {
            string query = "select * from usuarios where id = @id and unsub_date is null;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var res = conn.QueryFirstOrDefault<User>(query, new { id });
                return res;
            }
        }

        public int CreateUser(string name, int age, string mail, string password, string salt)
        {
            string query = "insert into usuarios (name, age, mail, password, salt) values (@name, @age, @mail, @password, @salt);";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var res = conn.Execute(query, new { name, age, mail, password, salt });
                if (res > 0)
                {
                    //usuario creado correctamente, recuperar su id
                    int user_id = conn.QuerySingle<int>("select last_insert_id();");
                    return user_id;
                }
                return res;
            }
        }

        public bool DeleteUser(int id, string date)
        {
            string verify_query = "select unsub_date from usuarios where id=@id;";
            string query = "update usuarios set unsub_date = @date where id=@id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var unsub_date_user = conn.QueryFirstOrDefault<string?>(verify_query, new { id });
                if (unsub_date_user != null)
                {
                    return false;
                }
                var res = conn.Execute(query, new { date, id });
                return res > 0;
            }
        }

        public int UpdateUser(int id, string name, int age)
        {
            string verify_query = "select unsub_date from usuarios where id=@id;";
            string query = "update usuarios set name=@name, age=@age where id=@id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var unsub_date_user = conn.QueryFirstOrDefault<string?>(verify_query, new { id });
                if (unsub_date_user != null)
                {
                    return 0;
                }
                var res = conn.Execute(query, new { name, age, id });
                return res;
            }
        }

        public bool IsMailInUse(string mail)
        {
            string query = "select mail from usuarios where mail = @mail and unsub_date is null";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var mail_in_use = conn.QueryFirstOrDefault<string>(query, new { mail });
                return mail_in_use != null;
            }
        }

        public User? GetUserByMail(string mail)
        {
            string query = "select * from usuarios where mail = @mail and unsub_date is null";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var res = conn.QueryFirstOrDefault<User>(query, new { mail });
                return res;
            }
        }
    }
}
