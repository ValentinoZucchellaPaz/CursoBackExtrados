using Data_Access.DAOCartas;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace APITorneo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartasController(IDAOCartas db) : Controller
    {

        private readonly IDAOCartas _db = db;

        [HttpGet("")]
        public async Task<IActionResult> GetCartas()
        {
            var cartas = await _db.GetCartasAsync();
            return Ok(cartas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarta(string id)
        {
            if (int.TryParse(id, out int value))
            {
                var carta = await _db.GetCartaByIdAsync(value);
                if (carta == null)
                {
                    return BadRequest("No hay ninguna serie con ese nombre");
                }
                return Ok(carta);
            }
            else
            {
                var carta = await _db.GetCartaByNameAsync(id);
                if (carta == null)
                {
                    return BadRequest("No hay ninguna serie con ese nombre");
                }
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
                if (serie == null)
                {
                    return BadRequest("No hay ninguna serie con ese nombre");
                }
                return Ok(serie);
            } else
            {
                var serie = await _db.GetSerieByNameAsync(id);
                if (serie == null)
                {
                    return BadRequest("No hay ninguna serie con ese nombre");
                }
                return Ok(serie);
            }
        }

        [HttpGet("series/{id}/all")]
        public async Task<IActionResult> GetCartasDeSerie(string id)
        {
            if(int.TryParse(id, out int value))
            {
                var serie_cards = await _db.GetSerieCardsAsync(value);
                return Ok(serie_cards);
            }
            else
            {
                var serie = await _db.GetSerieByNameAsync(id);
                if(serie == null)
                {
                    return BadRequest("No hay ninguna serie con ese nombre");
                }
                var serie_card = await _db.GetSerieCardsAsync(serie.Id);
                return Ok(serie_card);
            }

        }

    }
}
