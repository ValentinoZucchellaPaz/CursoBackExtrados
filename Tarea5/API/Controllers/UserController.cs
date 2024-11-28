using API.Services;
using DAO_Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Cryptography;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {

        private UserService _userService;
        public UsersController()
        {
            _userService = new UserService();
        }

        [HttpPost("login")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(MLogin login)
        {
            var (mail, password) = (login.Mail, login.Password);
            bool res = _userService.Login(mail, password);
            return res ? Ok(res) : BadRequest(new { message="Mail o contraseña incorrectos", success=false});
        }

        [HttpGet("search/all")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("search/{id}")]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return user == null ?
                NotFound(new { message = $"No se ha encontrado el usuario con id {id}", success = false })
                : Ok(user);
        }

        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(MId request)
        {
            bool response = _userService.DeleteUser(request);
            return response ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se ha encontrado ningun usuario activo con id {request.Id}", success = false });
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

        [HttpPost("update")]
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
