using DAO_Entidades.Entities;
using DAO_Entidades.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.AuthService;
using Services.UserService;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserService userService, IAuthService authService) : Controller
    {

        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(MLogin login)
        {
            var user = await _userService.Authenticate(login.Mail, login.Password);

            if (user == null) return Unauthorized(new { message = "Mail o contraseña incorrectos", success = false });

            var tokens = _authService.GenerateTokens(user.Id.ToString(), user.Mail, user.Role);

            return Ok(new
            {
                tokens,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Mail,
                    user.Role,
                }
            });
        }

        [HttpPost("logout")]
        public async Task<bool> Logout()
        {
            return false;
        }

        [HttpGet("search/all")]
        [Authorize]
        [ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("search/{id}")]
        [Authorize]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            return user == null ?
                NotFound(new { message = $"No se ha encontrado el usuario con id {id}", success = false })
                : Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(MCreateUser request)
        {
            int id = await _userService.CreateUser(request);

            return id == 0 ? 
                BadRequest(new { message = "Hubo un problema con la base de datos", success = false })
                : Ok(new { message = $"Usuario creado exitosamente con id {id}", success = true });
        }

        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(MId request)
        {
            bool deleted = await _userService.DeleteUser(request);
            return deleted ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se pudo eliminar al usuario con id {request.Id}", success = false });

        }

        [HttpPost("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(MUpdateUser request)
        {
            //si se actuliza correctamente se recupera la id de usuario
            int response = await _userService.UpdateUser(request);

            return response == 0 ?
                NotFound("El usuario que quiere actualizar no se encuentra activo")
                : Ok(new
                {
                    message = $"Usuario con id {response} actualizado correctamente con los valores: " +
                        $"Name: {request.Name} - Age: {request.Age}",
                    success = true
                });
        }
    }
}
