using DAO_Entidades.DTOs.Token;
using DAO_Entidades.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
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
        public async Task<IActionResult> Login(DTOLogin login)
        {
            var user = await _userService.Authenticate(login.Mail, login.Password);

            if (user == null) return Unauthorized(new { message = "Mail o contraseña incorrectos", success = false });

            var tokens = await _authService.GenerateTokens(user.Id, user.Mail, user.Role);

            Response.Cookies.Append("refresh-token", $"{user.Id}:{tokens.RefreshToken}", new CookieOptions()
            {
                Secure = true,
                Expires=tokens.RefreshTokenExpiration,
                HttpOnly=true,
                SameSite=SameSiteMode.Strict
            });

            return Ok(new DTOTokenResponse(tokens.AccessToken, tokens.AccessTokenExpiration, user));
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh()
        {
            //recupero valor de cookie
            if (!Request.Cookies.TryGetValue("refresh-token", out var refreshTokenCookie) || refreshTokenCookie == null)
            {
                return Unauthorized("No se encontró ningun refresh token, haga un login");
            }

            var parts = refreshTokenCookie.Split(":"); //valido que tenga el id y el refresh token
            if (parts.Length != 2) return Unauthorized("Formato de refresh token invalido, haga un login");

            var userId = int.Parse(parts[0]);
            string refreshToken = parts[1] ?? throw new ArgumentNullException("Refresh token null");

            var isValid = await _authService.ValidateRefreshToken(userId, refreshToken);

            if (!isValid) return Unauthorized("RefreshToken no valido");

            var user = await _userService.GetUser(userId) 
                ?? throw new Exception("User id no valido");

            //elimino refresh token y creo uno nuevo
            var deleted = await _authService.DeleteRefreshToken(userId, refreshToken);
            if (!deleted) throw new Exception("error eliminando el refresh token");

            var tokens = await _authService.GenerateTokens(userId, user.Mail, user.Role);

            //devuelvo access token en response y refresh token en cookie
            Response.Cookies.Append("refresh-token", $"{user.Id}:{tokens.RefreshToken}", new CookieOptions(){
                Secure = true,
                Expires = tokens.RefreshTokenExpiration,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new DTOTokenResponse(tokens.AccessToken, tokens.AccessTokenExpiration, user));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            //recupero valor de cookie
            if (!Request.Cookies.TryGetValue("refresh-token", out var refreshTokenCookie) || refreshTokenCookie == null)
            {
                return Unauthorized("No se encontró ningun refresh token, haga un login");
            }

            var parts = refreshTokenCookie.Split(":"); //valido que tenga el id y el refresh token
            if (parts.Length != 2) return Unauthorized("Formato de refresh token invalido, haga un login");

            var userId = int.Parse(parts[0]);
            string refreshToken = parts[1] ?? throw new ArgumentNullException("Refresh token null");

            // elimino de db
            var res = await _authService.DeleteRefreshToken(userId, refreshToken);
            if (!res) throw new Exception("hubo problemas para cerrar la sesion");

            //elimino cookie y devuelvo data
            Response.Cookies.Append("refresh-token", "", new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok($"sesion del usuario con id {userId} cerrada exitosamente");
        }

        [HttpGet("search/all")]
        [Authorize]
        [ProducesResponseType<List<DTOUser>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("search/{id}")]
        [Authorize]
        [ProducesResponseType<DTOUser>(StatusCodes.Status200OK)]
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
        public async Task<IActionResult> CreateUser(DTOCreateUser request)
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
        public async Task<IActionResult> DeleteUser(DTOId request)
        {
            bool deleted = await _userService.DeleteUser(request);
            return deleted ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se pudo eliminar al usuario con id {request.Id}", success = false });

        }

        [HttpPost("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(DTOUpdateUser request)
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
