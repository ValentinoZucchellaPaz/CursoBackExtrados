using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.AuthService;
using Services.UsuarioService;

namespace APITorneo.Controllers
{
    [ApiController] //preguntar a simon si auth debe validar solo usuarios y trabajar admins,orgs en db
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

        [HttpPost("sign-up")] //solo se pueden crear jugadores?? O se deberia poder crear jueces, org, admins desde otro endpoint??
        public async Task<IActionResult> CreateUser(DTOSignUp request)
        {
            // si solo voy a crear jugadores debo cambiar dtosignup para hacer que algunso campos sean obligatorios
            var res = await _userService.CrearUsuario(request);
            return Ok(res);
        }

        [HttpPost("borrar-usuario")]
        [Authorize(Roles = "admin")]
        public IActionResult BorrarUsuario()
        {
            return Ok("estamos joya");
        }

        [HttpPost("crear-admin")] //se debe poder crear uno desde 0 o actualizar otros ya existentes?? por ahora de la primera forma
        [Authorize(Roles = "admin")]
        public IActionResult CrearAdmin()
        {
            return View();
        }

        [HttpPost("crear-organizador")]
        [Authorize(Roles = "admin")]
        public IActionResult CrearOrganizador()
        {
            return View();
        }

        [HttpPost("crear-juez")]
        [Authorize(Roles = "organizador, admin")]
        public IActionResult CrearJuez()
        {
            return View();
        }
    }
}
