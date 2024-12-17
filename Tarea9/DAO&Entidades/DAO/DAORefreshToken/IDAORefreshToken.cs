using DAO_Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.DAO.DAORefreshToken
{
    public interface IDAORefreshToken
    {
        Task<RefreshToken?> GetRefreshToken(string userId, string token);
        Task<bool> SaveRefreshToken(RefreshToken token);
        Task<bool> RevokeRefreshToken(string userId, string token);
    }
}
