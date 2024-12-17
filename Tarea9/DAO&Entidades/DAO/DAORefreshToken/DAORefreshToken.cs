using DAO_Entidades.Entities;
using Dapper;
using MySqlConnector;

namespace DAO_Entidades.DAO.DAORefreshToken
{
    public class DAORefreshToken(string connectionString) : IDAORefreshToken
    {
        private readonly string _connectionString = connectionString;
        public async Task<RefreshToken?> GetRefreshToken(string userId, string refreshToken)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string queryGetToken = "select * from refresh_tokens where userId=@userId and token=@refreshToken";
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<RefreshToken>(queryGetToken, new { userId, refreshToken });
            }
        }

        public async Task<bool> SaveRefreshToken(RefreshToken refreshToken)
        {
            string querySaveToken = "insert into refresh_tokens (user_id, token, expiration_date, is_revoked) values (@UserId, @Token, @ExpirationDate, @IsRevoked)";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(querySaveToken, new
                {
                    refreshToken.UserId,
                    refreshToken.Token,
                    refreshToken.ExpirationDate,
                    refreshToken.IsRevoked,
                });
                return res > 0;
            }
        }

        public async Task<bool> RevokeRefreshToken(string userId, string refreshToken)
        {
            string queryUpdateRevoked = "update refresh_tokens set is_revoked=true where user_id=@userId and token=@refreshToken";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.ExecuteAsync(queryUpdateRevoked, new { userId, refreshToken });
                return res > 0;
            }
        }
    }
}
