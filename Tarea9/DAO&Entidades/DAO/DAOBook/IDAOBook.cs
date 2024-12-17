using DAO_Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Entidades.DAO.DAOBook
{
    public interface IDAOBook
    {
        public Task<IEnumerable<Book>> GetBooks();
        public Task<Book?> GetBook(string bookname);
        public Task<bool> RentBook(string bookName, DateTime rentDate, DateTime expirationDate, int userId);
    }
}
