using Models.Entidades;
using Dapper;
using MySqlConnector;

namespace Data_Access.DAOCartas
{
    public class DAOCartas(string connectionString) : IDAOCartas
    {
        private readonly string _connectionString = connectionString;


        /* CARTAS */
        public async Task<IEnumerable<Carta>> GetCartasAsync()
        {
            var queryGetCartas = "select * from cartas";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Carta>(queryGetCartas);
            }
        }
        public async Task<Carta?> GetCartaByIdAsync(int id)
        {
            var queryGetCarta = "select * from cartas where id=@id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Carta>(queryGetCarta, new { id });
            }
        }
        public async Task<Carta?> GetCartaByNameAsync(string nombre)
        {
            var queryGetCarta = "select * from cartas where nombre=@nombre";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Carta>(queryGetCarta, new { nombre });
            }
        }


        /* SERIES */
        public async Task<IEnumerable<Serie>> GetSeriesAsync()
        {
            var queryGetSeries = "select * from series";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Serie>(queryGetSeries);
            }
        }
        public async Task<Serie?> GetSerieByIdAsync(int id)
        {
            var queryGetSerie = "select * from series where id=@id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Serie>(queryGetSerie, new { id });
            }
        }
        public async Task<Serie?> GetSerieByNameAsync(string nombre)
        {
            var queryGetSerie = "select * from series where nombre=@nombre";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Serie>(queryGetSerie, new { nombre });
            }
        }
        public async Task<IEnumerable<Carta>> GetSerieCardsAsync(int id_serie)
        {
            var queryGetSerieCards = "select * from cartas where id in (select id_carta from cartas_por_serie where id_serie=@id_serie)";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Carta>(queryGetSerieCards, new { id_serie });
            }
        }
    }
}
