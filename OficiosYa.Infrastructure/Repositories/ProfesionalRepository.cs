using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private readonly OficiosYaDbContext _context;

        public ProfesionalRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task<Profesional?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                .FirstOrDefaultAsync(p => p.Usuario.Id == usuarioId);
        }

        public async Task<Profesional?> ObtenerPorIdAsync(int id)
        {
            return await _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, double? lat, double? lng, double? maxDist, int? minimoRating)
        {
            var query = _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                .AsQueryable();

            // Filtro por Oficio
            if (!string.IsNullOrEmpty(oficio))
            {
                query = query.Where(p => p.Oficios != null && p.Oficios.Any(po => po.Oficio.Nombre.Contains(oficio)));
            }

            // Filtro por Rating
            if (minimoRating.HasValue)
            {
                query = query.Where(p => p.RatingPromedio >= minimoRating.Value);
            }

            // Filtro por Ubicación (Aproximación simple)
            if (lat.HasValue && lng.HasValue && maxDist.HasValue)
            {
                // Nota: Esto es una aproximación muy básica. Para producción usar PostGIS.
                // 1 grado lat ~= 111km. 
                double deltaLat = maxDist.Value / 111.0;
                double deltaLng = maxDist.Value / (111.0 * Math.Cos(lat.Value * Math.PI / 180.0));

                var minLat = lat.Value - deltaLat;
                var maxLat = lat.Value + deltaLat;
                var minLng = lng.Value - deltaLng;
                var maxLng = lng.Value + deltaLng;

                // Necesitamos unir con UbicacionProfesional
                // Como no hay propiedad de navegación directa en Profesional hacia UbicacionProfesional (parece ser 1 a 1 o 1 a N pero no está en la entidad Profesional),
                // haremos un join manual o subquery.
                
                // Revisando la entidad Profesional, no tiene propiedad Ubicacion.
                // Revisando UbicacionProfesional, tiene ProfesionalId.
                
                var profesionalesConUbicacion = _context.UbicacionesProfesionales
                    .Where(u => u.Latitud >= minLat && u.Latitud <= maxLat && 
                                u.Longitud >= minLng && u.Longitud <= maxLng)
                    .Select(u => u.ProfesionalId);

                query = query.Where(p => profesionalesConUbicacion.Contains(p.Id));
            }

            return await query.ToListAsync();
        }

        public async Task AgregarAsync(Profesional profesional)
        {
            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profesional profesional)
        {
            _context.Profesionales.Update(profesional);
            await _context.SaveChangesAsync();
        }
    }
}
