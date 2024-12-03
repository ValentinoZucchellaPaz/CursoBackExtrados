using API.Services;
using DAO_Entidades.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {

        private UserService _userService;
        private AuthService _authService;
        public UsersController()
        {
            _userService = new UserService();
            _authService = new AuthService("secret_secret_secret_secret_secret", "http://localhost");
        }

        [HttpPost("login")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(MLogin login)
        {
            var user = _userService.Authenticate(login.Mail, login.Password);

            if (user == null) return Unauthorized(new { message = "Mail o contraseña incorrectos", success = false });

            var token = _authService.GenerateJwtToken(user.Id.ToString(), user.Mail);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Mail
                }
            });
        }

        [HttpGet("search/all")]
        [Authorize]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
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
            switch(id)
            {
                case -3:
                    return BadRequest(new { message = "No se ha pueden crear usuarios menores a 14 años", success = false });
                case -2:
                    return BadRequest(new { message = "Solo se pueden crear usuarios con mail @gmail.com", success = false });
                case -1:
                    return BadRequest(new { message = "El mail ya está en uso por otro usuario activo", success = false });
                case 0:
                    return BadRequest(new { message="Hubo un problema con la base de datos", success=false });
                default:
                    return Ok(new { message = $"Usuario creado exitosamente con id {id}", success = true });
            }
        }

        [HttpPost("delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(MId request)
        {
            bool response = _userService.DeleteUser(request);
            return response ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se ha encontrado ningun usuario activo con id {request.Id}", success = false });
        }

        [HttpPost("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(MUpdateUser request)
        {
            int response = _userService.UpdateUser(request);
            switch (response)
            {
                case -1:
                    return BadRequest(new { message = "El usuario no puede ser menor a 14 años", success = false });
                case 0:
                    return NotFound(new { message=$"No se ha encontrado ningun usuario con id {request.Id}"});
                default:
                    return Ok(new
                    {
                        message = $"Usuario actualizado correctamente con los valores" +
                        $"Name: {request.Name} - Age: {request.Age}",
                        success = true
                    });
            }
        }
    }
}
