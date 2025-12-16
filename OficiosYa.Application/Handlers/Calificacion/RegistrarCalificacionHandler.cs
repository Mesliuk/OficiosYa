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
        private readonly IProfesionalRepository _profesionalRepo;

        public RegistrarCalificacionHandler(IClasificacionRepository clasifRepo, IProfesionalRepository profesionalRepo)
        {
            _clasifRepo = clasifRepo;
            _profesionalRepo = profesionalRepo;
        }

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

            // Guardar la calificación
            await _clasifRepo.RegistrarAsync(dto);

            // Recuperar las calificaciones del profesional receptor
            // Se asume que el repositorio tiene un método que devuelve las calificaciones por receptor.
            var calificaciones = await _clasifRepo.ObtenerPorReceptorAsync(dto.ReceptorId);

            if (calificaciones == null)
            {
                // No hay calificaciones recuperadas; aseguramos valores por defecto
                await _profesionalRepo.ActualizarRatingAsync(dto.ReceptorId, 0d, 0);
                return;
            }

            var lista = calificaciones as IList<CalificacionDto> ?? calificaciones.ToList();

            // Calcular total y promedio
            var totalCalificaciones = lista.Count;
            var ratingPromedio = totalCalificaciones > 0 ? lista.Average(c => c.Puntaje) : 0d;

            // Actualizar los valores del profesional
            await _profesionalRepo.ActualizarRatingAsync(dto.ReceptorId, ratingPromedio, totalCalificaciones);
        }
    }
}
