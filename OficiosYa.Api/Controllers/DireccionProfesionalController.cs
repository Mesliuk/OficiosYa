using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Ubicacion;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionProfesionalController : ControllerBase
    {
        private readonly ActualizarUbicacionProfesionalHandler _updateHandler;
        private readonly IUbicacionRepository _ubicRepo;

        public DireccionProfesionalController(ActualizarUbicacionProfesionalHandler updateHandler, IUbicacionRepository ubicRepo)
        {
            _updateHandler = updateHandler;
            _ubicRepo = ubicRepo;
        }

        // POST api/direccionProfesional - create or update
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] UbicacionProfesionalDto dto)
        {
            await _updateHandler.HandleAsync(new OficiosYa.Application.Commands.Profesionales.ActualizarUbicacionProfesionalCommand
            {
                ProfesionalId = dto.ProfesionalId,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud
            });

            return Ok();
        }

        // GET api/direccionProfesional/{profesionalId}
        [HttpGet("{profesionalId}")]
        public async Task<IActionResult> GetByProfesional(int profesionalId)
        {
            var ubicacion = await _ubicRepo.GetByProfesionalAsync(profesionalId);
            if (ubicacion == null) return NotFound();
            return Ok(ubicacion);
        }

        // PUT api/direccionProfesional/{profesionalId}
        [HttpPut("{profesionalId}")]
        public async Task<IActionResult> Update(int profesionalId, [FromBody] UbicacionProfesionalDto dto)
        {
            if (profesionalId != dto.ProfesionalId) return BadRequest();
            await _updateHandler.HandleAsync(new OficiosYa.Application.Commands.Profesionales.ActualizarUbicacionProfesionalCommand
            {
                ProfesionalId = dto.ProfesionalId,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud
            });
            return NoContent();
        }

        // DELETE api/direccionProfesional/{profesionalId}
        [HttpDelete("{profesionalId}")]
        public async Task<IActionResult> Delete(int profesionalId)
        {
            await _ubicRepo.DeleteByProfesionalAsync(profesionalId);
            return NoContent();
        }
    }
}
