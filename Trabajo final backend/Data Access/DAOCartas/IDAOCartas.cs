using Models.Entidades;

namespace Data_Access.DAOCartas
{
    public interface IDAOCartas
    {
        public Task<IEnumerable<Carta>> GetCartasAsync();
        public Task<Carta?> GetCartaByIdAsync(int id);
        public Task<Carta?> GetCartaByNameAsync(string nombre);
        public Task<IEnumerable<Serie>> GetSeriesAsync();
        public Task<Serie?> GetSerieByIdAsync(int id);
        public Task<Serie?> GetSerieByNameAsync(string nombre);
        public Task<IEnumerable<Carta>> GetSerieCardsAsync(int id_serie);
    }
}
