using Microsoft.AspNetCore.Mvc;
using OficiosYa.Api.Models;
using OficiosYa.Application.Commands.Calificaciones;
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
        public async Task<IActionResult> CrearCalificacion(CalificacionRequest request)
        {
            var command = new CrearCalificacionCommand
            {
                UsuarioCalificaId = request.EmisorId,
                UsuarioCalificadoId = request.ReceptorId,
                Puntaje = request.Puntaje,
                Comentario = request.Comentario
            };

            await _registrarCalificacionHandler.HandleAsync(command);

            var dto = new OficiosYa.Application.DTOs.CalificacionDto
            {
                EmisorId = command.UsuarioCalificaId,
                ReceptorId = command.UsuarioCalificadoId,
                Puntaje = command.Puntaje,
                Comentario = command.Comentario
            };

            return Ok(dto);
        }
    }
}

