using API.Services.AuthService;
using API.Services.UserService;
using DAO_Entidades.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService, IAuthService authService) : Controller
    {

        private IUserService _userService = userService;
        private IAuthService _authService = authService;

        [HttpPost("login")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(MLogin login)
        {
            var user = _userService.Authenticate(login.Mail, login.Password);

            if (user == null) return Unauthorized(new { message = "Mail o contraseña incorrectos", success = false });

            var token = _authService.GenerateJwtToken(user.Id.ToString(), user.Mail, user.Role);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Mail,
                    user.Role,
                }
            });
        }

        [HttpGet("search/all")]
        [Authorize]
        [ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("search/{id}")]
        [Authorize]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return user == null ?
                NotFound(new { message = $"No se ha encontrado el usuario con id {id}", success = false })
                : Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser(MCreateUser request)
        {
            int id = _userService.CreateUser(request);

            return id == 0 ? 
                BadRequest(new { message = "Hubo un problema con la base de datos", success = false })
                : Ok(new { message = $"Usuario creado exitosamente con id {id}", success = true });
        }

        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(MId request)
        {
            bool deleted = _userService.DeleteUser(request);
            return deleted ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se pudo eliminar al usuario con id {request.Id}", success = false });

        }

        [HttpPost("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(MUpdateUser request)
        {
            //si se actuliza correctamente se recupera la id de usuario
            int response = _userService.UpdateUser(request);

            return response == 0 ?
                NotFound("El usuario que quiere actualizar no se encuentra activo")
                : Ok(new
                {
                    message = $"Usuario con id {response} actualizado correctamente con los valores: " +
                        $"Name: {request.Name} - Age: {request.Age}",
                    success = true
                });
        }

        [HttpGet("book")]
        [Authorize(Roles ="user")]
        public IActionResult GetBooks()
        {
            var res = _userService.GetBooks();
            return Ok(res);
        }

        [HttpPost("rent-book")]
        [Authorize(Roles = "user")]
        public IActionResult RentBook(MBook bookName)
        {
            var res = _userService.RentBook(bookName.Name);
            return Ok(res);
        }
    }
}
