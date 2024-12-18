using DAO_Entidades.Entities;
using Dapper;
using MySqlConnector;
using System.Linq;

namespace DAO_Entidades.DAO.DAORefreshToken
{
    public class DAORefreshToken(string connectionString) : IDAORefreshToken
    {
        private readonly string _connectionString = connectionString;
        public async Task<RefreshToken?> GetRefreshToken(int userId, string refreshToken)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string queryGetToken = "select * from refresh_tokens where user_id=@userId and token=@refreshToken";
                await conn.OpenAsync();
                var token =  await conn.QueryFirstOrDefaultAsync<RefreshToken>(queryGetToken, new { userId, refreshToken });
                return token;
            }
        }

        public async Task<bool> SaveRefreshToken(int userId, string token, DateTime expirationDate)
        {
            string querySaveToken = "insert into refresh_tokens (user_id, token, expiration_date, is_revoked) values (@userId, @token, @expirationDate, false)";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(querySaveToken, new
                {
                    userId,
                    token,
                    expirationDate
                });
                return res > 0;
            }
        }

        public async Task<bool> DeleteRefreshToken(int userId, string refreshToken)
        {
            string queryDeleteToken = "update refresh_tokens set is_revoked=true where user_id=@userId and token=@refreshToken";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryDeleteToken, new { userId, refreshToken });
                return res > 0;
            }
        }
    }
}
