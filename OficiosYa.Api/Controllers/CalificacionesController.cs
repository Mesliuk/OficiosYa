using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OficiosYa.Application.Handlers.Calificacion;
using OficiosYa.Application.Interfaces;
using OficiosYa.Api.Models;
using OficiosYa.Application.Commands.Calificaciones;
using OficiosYa.Application.DTOs;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalificacionesController : ControllerBase
    {
        private readonly RegistrarCalificacionHandler _registrarCalificacionHandler;
        private readonly IClasificacionRepository _calificacionRepository;

        public CalificacionesController(
            RegistrarCalificacionHandler registrarCalificacionHandler,
            IClasificacionRepository calificacionRepository)
        {
            _registrarCalificacionHandler = registrarCalificacionHandler;
            _calificacionRepository = calificacionRepository;
        }

        [HttpGet("{receptorId}")]
        public async Task<IActionResult> GetPorReceptor(int receptorId)
        {
            try
            {
                var calificaciones = await _calificacionRepository.ObtenerPorReceptorAsync(receptorId);
                return Ok(calificaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error al obtener las calificaciones.",
                    Detail = ex.Message
                });
            }
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
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error al registrar la calificación.",
                    Detail = ex.Message
                });
            }
        }
    }
}


