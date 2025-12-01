using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Rubros;
using OficiosYa.Application.DTOs;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RubrosController : ControllerBase
    {
        private readonly GetRubrosHandler _getRubrosHandler;

        public RubrosController(GetRubrosHandler getRubrosHandler)
        {
            _getRubrosHandler = getRubrosHandler;
        }

        // GET api/rubros
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null, [FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            var result = await _getRubrosHandler.HandleAsync(search, page, size);
            return Ok(result);
        }

        // GET api/rubros/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rubro = await _getRubrosHandler.HandleByIdAsync(id);
            if (rubro == null) return NotFound();
            return Ok(rubro);
        }
    }
}

