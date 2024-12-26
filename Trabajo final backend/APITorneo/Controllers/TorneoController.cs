using DAO_Entidades.DAO.DAOCartas;
using Microsoft.AspNetCore.Mvc;

namespace APITorneo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TorneoController(IDAOCartas db) : Controller
    {
        private readonly IDAOCartas _db = db;

        [HttpGet("cartas")]
        public async Task<IActionResult> GetCartas()
        {
            var cartas = await _db.GetCartasAsync();
            return Ok(cartas);
        }

        [HttpGet("cartas/{id}")]
        public async Task<IActionResult> GetCarta(int id)
        {
            Console.WriteLine("en el endpoint");
            var carta = await _db.GetCartaAsync(id);
            return Ok(carta);
        }

        [HttpGet("generacion/{id}")]
        public async Task<IActionResult> GetGeneracion(int id)
        {
            var gen = await _db.GetGeneracionAsync(id);
            return Ok(gen);
        }
    }
}
