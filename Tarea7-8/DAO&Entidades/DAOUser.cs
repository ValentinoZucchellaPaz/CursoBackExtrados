using MySqlConnector;
using Dapper;
using System.Data;
using System.Xml.Linq;
using DAO_Entidades.Models;
using System.Security.Cryptography.X509Certificates;

namespace DAO_Entidades
{
    public class DAOUser (string connectionString): IDAOUser
    {

        private readonly string _connectionString = connectionString;
        private readonly string queryGetAllUsers = "select * from usuarios;";
        private readonly string queryGetUser = "select * from usuarios where id = @id;";
        private readonly string queryCreateUser = "insert into usuarios (name, age, mail, password, salt, role) values (@name, @age, @mail, @password, @salt, @role);";
        private readonly string queryVerifyUnsub = "select unsub_date from usuarios where id=@id;";
        private readonly string queryDeleteUser = "update usuarios set unsub_date = @date where id=@id;";
        private readonly string queryUpdateUser = "update usuarios set name=@name, age=@age where id=@id;";
        private readonly string queryGetActiveMails = "select mail from usuarios where mail = @mail and unsub_date is null";
        private readonly string queryGetUserByMail = "select * from usuarios where mail = @mail and unsub_date is null";
        private readonly string queryRentBook = "update libros set rent_date=@rentDate, expiration_date=@expirationDate, user_id=@userId where name=@bookName";
        private readonly string queryGetBookByName = "select * from libros where name=@bookName";

        public List<User> GetAllUsers()
        {
            //trae usuarios activos
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.Query<User>(queryGetAllUsers).ToList();
                return res;
            }
        }

        public User? GetUser(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.QueryFirstOrDefault<User>(queryGetUser, new { id });
                return res;
            }
        }

        public int CreateUser(string name, int age, string mail, string password, string salt, string role)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.Execute(queryCreateUser, new { name, age, mail, password, salt, role });
                if (res == 0)
                {
                    return 0;
                }
                //usuario creado correctamente, recuperar su id
                int user_id = conn.QuerySingle<int>("select last_insert_id();");
                return user_id;
            }
        }

        public bool DeleteUser(int id, DateTime date)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var unsub_date_user = conn.QueryFirstOrDefault<string?>(queryVerifyUnsub, new { id });
                if (unsub_date_user != null)
                {
                    return false;
                }
                var res = conn.Execute(queryDeleteUser, new { date, id });
                return res > 0;
            }
        }

        public int UpdateUser(int id, string name, int age)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var unsub_date_user = conn.QueryFirstOrDefault<string?>(queryVerifyUnsub, new { id });
                if (unsub_date_user != null)
                {
                    return 0;
                }
                var res = conn.Execute(queryUpdateUser, new { name, age, id });
                return res;
            }
        }

        public bool IsMailInUse(string mail)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var mail_in_use = conn.QueryFirstOrDefault<string>(queryGetActiveMails, new { mail });
                return mail_in_use != null;
            }
        }

        public User? GetUserByMail(string mail)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.QueryFirstOrDefault<User>(queryGetUserByMail, new { mail });
                return res;
            }
        }

        public bool RentBook(int id, string book, string rentDate, string expirationDate)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.Execute(queryRentBook, new { id, rentDate, expirationDate, name = book });
                return res > 0;
            }
        }
        public List<Book> GetBooks()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.Query<Book>("select * from libros").ToList();
                return res;
            }
        }

        public Book? GetBook(string bookName)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.QueryFirstOrDefault<Book>(queryGetBookByName, new { bookName });
                return res;
            }
        }

        public bool RentBook(string bookName, DateTime rentDate, DateTime expirationDate, int userId)
        {
            //buscar libro y actualizar
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var res = conn.Execute(queryRentBook, new { rentDate, expirationDate, userId, bookName});
                return res > 0;
            }
        }
    }
}
