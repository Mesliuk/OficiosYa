using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly OficiosYaDbContext _context;

        public UbicacionRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarUbicacionAsync(UbicacionProfesionalDto dto)
        {
            var ubicacion = await _context.UbicacionesProfesionales
                .FirstOrDefaultAsync(u => u.ProfesionalId == dto.ProfesionalId);

            if (ubicacion == null)
            {
                ubicacion = new UbicacionProfesional
                {
                    ProfesionalId = dto.ProfesionalId,
                    Latitud = dto.Latitud,
                    Longitud = dto.Longitud,
                    UltimaActualizacion = DateTime.UtcNow
                };
                _context.UbicacionesProfesionales.Add(ubicacion);
            }
            else
            {
                ubicacion.Latitud = dto.Latitud;
                ubicacion.Longitud = dto.Longitud;
                ubicacion.UltimaActualizacion = DateTime.UtcNow;
                _context.UbicacionesProfesionales.Update(ubicacion);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<UbicacionProfesionalDto?> GetByProfesionalAsync(int profesionalId)
        {
            var ubicacion = await _context.UbicacionesProfesionales
                .FirstOrDefaultAsync(u => u.ProfesionalId == profesionalId);

            if (ubicacion == null) return null;

            return new UbicacionProfesionalDto
            {
                ProfesionalId = ubicacion.ProfesionalId,
                Latitud = ubicacion.Latitud,
                Longitud = ubicacion.Longitud
            };
        }

        public async Task DeleteByProfesionalAsync(int profesionalId)
        {
            var ubicacion = await _context.UbicacionesProfesionales
                .FirstOrDefaultAsync(u => u.ProfesionalId == profesionalId);
            if (ubicacion == null) return;

            _context.UbicacionesProfesionales.Remove(ubicacion);
            await _context.SaveChangesAsync();
        }
    }
}
