using DAO_Entidades.Entities;
using Dapper;
using MySqlConnector;


namespace DAO_Entidades.DAO.DAOBook
{
    public class DAOBook (string connectionString) : IDAOBook
    {

        private readonly string _connectionString = connectionString;
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
            string queryGetBookByName = "select * from libros where name=@bookName";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<Book>(queryGetBookByName, new { bookName });
            }
        }

        public async Task<bool> RentBook(string bookName, DateTime rentDate, DateTime expirationDate, int userId)
        {
            string queryRentBook = "update libros set rent_date=@rentDate, expiration_date=@expirationDate, user_id=@userId where name=@bookName";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryRentBook, new { rentDate, expirationDate, userId, bookName });
                return res > 0;
            }
        }
    }
}
