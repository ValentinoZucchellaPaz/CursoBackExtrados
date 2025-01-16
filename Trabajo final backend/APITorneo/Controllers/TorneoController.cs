using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITorneo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TorneoController : Controller
    {
        [HttpPost("inscribir-jugador")]
        [Authorize(Roles = "jugador")]
        public IActionResult InscribirJugador()
        {

            //validar que usuario logeado sea jugador
            //mandar a hacerlo el dao en la db y retornar algo

            return View();
        }

        [HttpPost("iniciar-torneo")]
        [Authorize(Roles = "organizador")]
        public IActionResult AceptarInscripcion()
        {
            //validar torneo que se quiere aceptar este en modo inscripcion
            //validar que el organizador que usa el endpoint es el org del torneo
            //se copia la lista de inscripcion-jugadores a inscripcion-jugadores-aceptados
            //automaticamente pasar torneo a fase de juego

            //preguntar simon si debo ser tan granular o si acepto todos de una y ya

            return View();
        }

        [HttpPost("editar-torneo")]
        [Authorize("organizador")]
        public IActionResult EditarTorneo()
        {
            //pasar de fase y otras cosas
            return View();
        }

        [HttpGet("cancelar-torneo")]
        [Authorize(Roles = "admin")]
    }
}
