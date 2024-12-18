using DAO_Entidades.DAO.DAOBook;
using DAO_Entidades.DAO.DAOUser;
using DAO_Entidades.DTOs.Book;
using DAO_Entidades.Entities;
using Microsoft.AspNetCore.Http;
using Services.Security.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.BookService
{
    public  class BookService(IDAOBook db, IHttpContextAccessor httpContextAccessor) : IBookService
    {
        private readonly IDAOBook _db = db;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<List<DTOBookResponse>> GetBooks()
        {
            var books = await _db.GetBooks();
            //pasar a entidad a dto
            var DtoBookList = new List<DTOBookResponse>();
            foreach (var book in books)
            {
                DtoBookList.Add(new DTOBookResponse(book.Name, book.RentDate, book.ExpirationDate, book.UserId));
            }
            return DtoBookList;

        }

        public async Task<Book?> GetBook(string name)
        {
            return await _db.GetBook(name);
        }

        public async Task<bool> RentBook(string bookName)
        {
            var userId = GetUserIdFromClaims();

            // Verificar que no este alquilado en este momento el libro
            DateTime utcNow = DateTime.UtcNow;
            var bookToRent = await _db.GetBook(bookName) ?? throw new RentBookException("Este libro no existe, revise los nombres de libro existentes");
            if (bookToRent.ExpirationDate > utcNow && bookToRent.UserId != null)
            {
                throw new RentBookException($"Este libro esta siendo alquilado ahora mismo por el usuario {bookToRent.UserId} hasta la fecha {bookToRent.ExpirationDate}");
            }

            // Alquilar
            DateTime expirationDate = utcNow.AddDays(5);
            var res = await _db.RentBook(bookName, utcNow, expirationDate, userId);
            return res;
        }

        private int GetUserIdFromClaims()
        {
            // Obtener el ID del usuario desde los claims del token
            var userIdClaim = (_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid))
                ?? throw new UnauthorizedAccessException("El token no contiene información de usuario.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new InvalidOperationException("El ID del usuario en los claims no es válido.");
            }

            return userId;
        }
    }
}
