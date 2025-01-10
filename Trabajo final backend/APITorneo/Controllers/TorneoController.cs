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
        public async Task<IActionResult> GetCarta(string id)
        {
            if (int.TryParse(id, out int value))
            {
                var carta = await _db.GetCartaByIdAsync(value);
                return Ok(carta);
            }
            else
            {
                var carta = await _db.GetCartaByNameAsync(id);
                return Ok(carta);
            }
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetSeries()
        {
            var series = await _db.GetSeriesAsync();
            return Ok(series);
        }

        [HttpGet("series/{id}")]
        public async Task<IActionResult> GetSerie(string id)
        {
            if(int.TryParse(id, out int value))
            {
                var serie = await _db.GetSerieByIdAsync(value);
                return Ok(serie);
            } else
            {
                var serie = await _db.GetSerieByNameAsync(id);
                return Ok(serie);
            }
        }

        [HttpGet("series/{id}/cartas")]
        public async Task<IActionResult> GetCartasDeSerie(int id)
        {
            var serie = await _db.GetSerieCardsAsync(id);
            return Ok(serie);
        }

    }
}
