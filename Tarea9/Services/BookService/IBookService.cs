using DAO_Entidades.DTOs.Book;
using DAO_Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookService
{
    public interface IBookService
    {
        public Task<List<DTOBookResponse>> GetBooks();
        public Task<Book?> GetBook(string name);
        public Task<bool> RentBook(string bookName);
    }
}
