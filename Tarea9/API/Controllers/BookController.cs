using DAO_Entidades.DTOs.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.BookService;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController(IBookService bookService) : Controller
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet("all")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetBooks()
        {
            var res = await _bookService.GetBooks();
            return Ok(res);
        }

        //TODO: arreglar
        //[HttpGet("/{name}")]
        //[Authorize(Roles ="user")]
        //public async Task<IActionResult> GetBook(string name)
        //{
        //    Console.WriteLine(name);
        //    var book = await _bookService.GetBook(name);
        //    return book == null ?
        //        NotFound(new { message = $"No se ha encontrado el libro: {name}", success = false })
        //        :Ok(book);
        //}

        [HttpPost("rent-book")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> RentBook(DTOBookRequest bookName)
        {
            var res = await _bookService.RentBook(bookName.Name);
            return Ok(res);
        }
    }
}
