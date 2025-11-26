using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Ubicacion;
using OficiosYa.Application.DTOs;

namespace OficiosYa.Api.Controllers
{

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionController : ControllerBase
    {
        private readonly RegisterUbicacionHandler _registerHandler;
        private readonly GetUbicacionHandler _getHandler;

        public UbicacionController(RegisterUbicacionHandler registerHandler, GetUbicacionHandler getHandler)
        {
            _registerHandler = registerHandler;
            _getHandler = getHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] UbicacionProfesionalDto dto)
        {
            await _registerHandler.HandleAsync(dto);
            return Ok();
        }

        [HttpGet("{profesionalId}")]
        public async Task<IActionResult> GetByProfesional(int profesionalId)
        {
            var ubicacion = await _getHandler.HandleAsync(profesionalId);
            if (ubicacion == null) return NotFound();
            return Ok(ubicacion);
        }
    }
}
}
