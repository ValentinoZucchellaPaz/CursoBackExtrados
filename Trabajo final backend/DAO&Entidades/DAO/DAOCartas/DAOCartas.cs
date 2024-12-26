using DAO_Entidades.Entidades;
using Dapper;
using MySqlConnector;

namespace DAO_Entidades.DAO.DAOCartas
{
    public class DAOCartas(string connectionString) : IDAOCartas
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Carta>> GetCartasAsync()
        {
            var queryGetCartas = "select * from pokemonbase";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Carta>(queryGetCartas);
            }
        }

        public async Task<Carta?> GetCartaAsync(int id)
        {
            var queryGetCarta = "select * from pokemonbase where id=@id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Carta>(queryGetCarta, new { id });
            }
        }

        public async Task<Generacion?> GetGeneracionAsync(int id)
        {
            var queryGetGeneracion = "select * from generacion where id=@id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Generacion>(queryGetGeneracion, new { id });
            }
        }
    }
}
