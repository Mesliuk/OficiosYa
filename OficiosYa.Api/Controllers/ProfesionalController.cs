using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Profesional;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Api.Controllers
{


namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private readonly GetProfesionalByIdHandler _getHandler;
        private readonly UpdateProfesionalHandler _updateHandler;
        private readonly SearchProfesionalHandler _searchHandler;

        public ProfesionalController(
            GetProfesionalByIdHandler getHandler,
            UpdateProfesionalHandler updateHandler,
            SearchProfesionalHandler searchHandler)
        {
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _searchHandler = searchHandler;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var profesional = await _getHandler.HandleAsync(usuarioId);
            if (profesional == null) return NotFound();
            return Ok(profesional);
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody] UpdateProfesionalRequest request)
        {
            var profesional = await _getHandler.HandleAsync(usuarioId);
            if (profesional == null) return NotFound();

            // Update fields
            profesional.Bio = request.Bio;
            profesional.Documento = request.Documento;
            // Update Usuario fields if needed, e.g. FotoPerfil
            if (profesional.Usuario != null)
            {
                profesional.Usuario.Nombre = request.Nombre;
                profesional.Usuario.Apellido = request.Apellido;
                profesional.Usuario.Telefono = request.Telefono;
                profesional.Usuario.FotoPerfil = request.FotoPerfil;
            }

            await _updateHandler.HandleAsync(profesional);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? oficio, [FromQuery] double? lat, [FromQuery] double? lng, [FromQuery] double? maxDist, [FromQuery] int? minimoRating)
        {
            var resultados = await _searchHandler.HandleAsync(oficio, lat, lng, maxDist, minimoRating);
            return Ok(resultados);
        }
    }

    public class UpdateProfesionalRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string? FotoPerfil { get; set; }
    }
}
}
