using OficiosYa.Application.Commands.Calificaciones;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Calificacion
{
    public class RegistrarCalificacionHandler
    {
        private readonly IClasificacionRepository _clasifRepo;
        public RegistrarCalificacionHandler(IClasificacionRepository repo) { _clasifRepo = repo; }
        public async Task HandleAsync(CrearCalificacionCommand command)
        {
            // Crear un DTO de calificación a partir del comando recibido
            var dto = new CalificacionDto
            {
                EmisorId = command.UsuarioCalificaId,
                ReceptorId = command.UsuarioCalificadoId,
                Puntaje = command.Puntaje,
                Comentario = command.Comentario
            };

            // Usar el método correcto del repositorio
            await _clasifRepo.RegistrarAsync(dto);
        }
    }
}
