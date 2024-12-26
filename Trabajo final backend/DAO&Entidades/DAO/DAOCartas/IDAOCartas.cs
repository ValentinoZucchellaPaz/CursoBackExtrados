using DAO_Entidades.Entidades;

namespace DAO_Entidades.DAO.DAOCartas
{
    public interface IDAOCartas
    {
        public Task<IEnumerable<Carta>> GetCartasAsync();
        public Task<Carta?> GetCartaAsync(int id);
        public Task<Generacion?> GetGeneracionAsync(int id);
    }
}
