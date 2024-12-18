using DAO_Entidades.Entities;

namespace DAO_Entidades.DAO.DAORefreshToken
{
    public interface IDAORefreshToken
    {
        Task<RefreshToken?> GetRefreshToken(int userId, string token);
        Task<bool> SaveRefreshToken(int userId, string token, DateTime expirationDate);
        Task<bool> DeleteRefreshToken(int userId, string token);
    }
}
