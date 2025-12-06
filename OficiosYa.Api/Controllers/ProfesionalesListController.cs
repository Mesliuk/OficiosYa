using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/profesionales")]
    public class ProfesionalesListController : ControllerBase
    {
        private readonly IProfesionalRepository _profRepo;

        public ProfesionalesListController(IProfesionalRepository profRepo)
        {
            _profRepo = profRepo;
        }

        // GET /api/profesionales/raw
        // Returns full Profesional entities from DB for inspection
        [HttpGet("raw")]
        [ProducesResponseType(typeof(IEnumerable<Profesional>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetRaw([FromQuery] string? oficio = null, [FromQuery] double? lat = null, [FromQuery] double? lng = null, [FromQuery] double? maxDist = null, [FromQuery] int? minimoRating = null)
        {
            var lista = await _profRepo.BuscarPorFiltrosAsync(oficio, lat, lng, maxDist, minimoRating);
            return Ok(lista);
        }
    }
}
