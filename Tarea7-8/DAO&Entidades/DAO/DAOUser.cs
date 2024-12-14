using MySqlConnector;
using Dapper;
using DAO_Entidades.Entities;

namespace DAO_Entidades.DAO
{
    public class DAOUser(string connectionString) : IDAOUser
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            //trae usuarios activos
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<User>(queryGetAllUsers);
            }
        }

        public async Task<User?> GetUser(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<User>(queryGetUser, new { id });
            }
        }

        public async Task<int> CreateUser(string name, int age, string mail, string password, string salt, string role)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryCreateUser, new { name, age, mail, password, salt, role });
                if (res == 0)
                {
                    return 0;
                }
                //usuario creado correctamente, recuperar su id
                return await conn.QuerySingleAsync<int>("select last_insert_id();");
            }
        }

        public async Task<bool> DeleteUser(int id, DateTime date)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var unsub_date_user = await conn.QueryFirstOrDefaultAsync<string?>(queryVerifyUnsub, new { id });
                if (unsub_date_user != null)
                {
                    return false;
                }
                var res = await conn.ExecuteAsync(queryDeleteUser, new { date, id });
                return res > 0;
            }
        }

        public async Task<int> UpdateUser(int id, string name, int age)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var unsub_date_user = await conn.QueryFirstOrDefaultAsync<string?>(queryVerifyUnsub, new { id });
                if (unsub_date_user != null)
                {
                    return 0;
                }
                return await conn.ExecuteAsync(queryUpdateUser, new { name, age, id });
            }
        }

        public async Task<bool> IsMailInUse(string mail)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var mail_in_use = await conn.QueryFirstOrDefaultAsync<string>(queryGetActiveMails, new { mail });
                return mail_in_use != null;
            }
        }

        public async Task<User?> GetUserByMail(string mail)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<User>(queryGetUserByMail, new { mail });
            }
        }

        public async Task<bool> RentBook(int id, string book, string rentDate, string expirationDate)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryRentBook, new { id, rentDate, expirationDate, name = book });
                return res > 0;
            }
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<Book>("select * from libros");
            }
        }

        public async Task<Book?> GetBook(string bookName)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<Book>(queryGetBookByName, new { bookName });
            }
        }

        public async Task<bool> RentBook(string bookName, DateTime rentDate, DateTime expirationDate, int userId)
        {
            //buscar libro y actualizar
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryRentBook, new { rentDate, expirationDate, userId, bookName });
                return res > 0;
            }
        }
    }
}
