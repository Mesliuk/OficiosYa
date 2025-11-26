using Microsoft.AspNetCore.Mvc;
using OficiosYa.Api.Models;
using OficiosYa.Application.Commands.Calificaciones;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Handlers.Calificacion;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalificacionesController : ControllerBase
    {
        private readonly RegistrarCalificacionHandler _registrarCalificacionHandler;

        public CalificacionesController(RegistrarCalificacionHandler registrarCalificacionHandler)
        {
            _registrarCalificacionHandler = registrarCalificacionHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCalificacion([FromBody] CalificacionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new CrearCalificacionCommand
                {
                    UsuarioCalificaId = request.EmisorId,
                    UsuarioCalificadoId = request.ReceptorId,
                    Puntaje = request.Puntaje,
                    Comentario = request.Comentario
                };

                await _registrarCalificacionHandler.HandleAsync(command);

                var dto = new CalificacionDto
                {
                    EmisorId = command.UsuarioCalificaId,
                    ReceptorId = command.UsuarioCalificadoId,
                    Puntaje = command.Puntaje,
                    Comentario = command.Comentario
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                // Podés crear un middleware de errores si querés
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error al registrar la calificación.",
                    Detail = ex.Message
                });
            }
        }
    }
}


