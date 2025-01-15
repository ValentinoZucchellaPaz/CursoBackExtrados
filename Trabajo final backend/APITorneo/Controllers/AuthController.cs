using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.AuthService;
using Services.UsuarioService;

namespace APITorneo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IUsuarioService user_service, IAuthService auth_service) : Controller
    {
        private readonly IUsuarioService _userService = user_service;
        private readonly IAuthService _authService = auth_service;
        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOLogin request)
        {
            // validar mail y contraseña
            var user = await _userService.Authenticate(request);
            if(user== null) return Unauthorized(new { message = "Mail o contraseña incorrectos", success = false });

            var token = _authService.GenerateJwtToken(user.Id.ToString(), user.Email, user.Role);

            Response.Cookies.Append("torneo-access-token", $"{user.Id}: {token}", new CookieOptions()
            {
                Secure = true,
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
            });

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Nombre,
                    user.Email,
                    user.Role,
                },
            });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> CreateUser(DTOSignUp request)
        {
            var res = await _userService.CrearUsuario(request);
            return Ok(res);
        }
    }
}
