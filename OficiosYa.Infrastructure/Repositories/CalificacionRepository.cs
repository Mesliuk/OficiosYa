using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class CalificacionRepository : IClasificacionRepository
    {
        private readonly OficiosYaDbContext _context;

        public CalificacionRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarAsync(CalificacionDto dto)
        {
            var calificacion = new Calificacion
            {
                ReceptorId = dto.ReceptorId,
                EmisorId = dto.EmisorId,
                Puntaje = dto.Puntaje,
                Comentario = dto.Comentario,
                Fecha = DateTime.UtcNow
            };

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CalificacionDto>> GetByReceptorAsync(int receptorId)
        {
            return await _context.Calificaciones
                .Where(c => c.ReceptorId == receptorId)
                .Select(c => new CalificacionDto
                {
                    ReceptorId = c.ReceptorId,
                    EmisorId = c.EmisorId,
                    Puntaje = c.Puntaje,
                    Comentario = c.Comentario
                })
                .ToListAsync();
        }

        public async Task<double> ObtenerPromedioAsync(int receptorId)
        {
            var calificaciones = await _context.Calificaciones
                .Where(c => c.ReceptorId == receptorId)
                .ToListAsync();

            if (!calificaciones.Any()) return 0;

            return calificaciones.Average(c => c.Puntaje);
        }
    }
}
