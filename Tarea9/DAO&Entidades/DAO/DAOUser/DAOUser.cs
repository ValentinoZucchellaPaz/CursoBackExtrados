using MySqlConnector;
using Dapper;
using DAO_Entidades.Entities;

namespace DAO_Entidades.DAO.DAOUser
{
    public class DAOUser(string connectionString) : IDAOUser
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            string queryGetAllUsers = "select * from usuarios;";
            //trae usuarios activos
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<User>(queryGetAllUsers);
            }
        }

        public async Task<User?> GetUser(int id)
        {
            string queryGetUser = "select * from usuarios where id = @id;";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<User>(queryGetUser, new { id });
            }
        }

        public async Task<int> CreateUser(string name, int age, string mail, string password, string salt, string role)
        {
            string queryCreateUser = "insert into usuarios (name, age, mail, password, salt, role) values (@name, @age, @mail, @password, @salt, @role);";
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
            string queryVerifyUnsub = "select unsub_date from usuarios where id=@id;";
            string queryDeleteUser = "update usuarios set unsub_date = @date where id=@id;";
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
            string queryVerifyUnsub = "select unsub_date from usuarios where id=@id;";
            string queryUpdateUser = "update usuarios set name=@name, age=@age where id=@id;";
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
            string queryGetActiveMails = "select mail from usuarios where mail = @mail and unsub_date is null";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var mail_in_use = await conn.QueryFirstOrDefaultAsync<string>(queryGetActiveMails, new { mail });
                return mail_in_use != null;
            }
        }

        public async Task<User?> GetUserByMail(string mail)
        {
            string queryGetUserByMail = "select * from usuarios where mail = @mail and unsub_date is null";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<User>(queryGetUserByMail, new { mail });
            }
        }
    }
}
