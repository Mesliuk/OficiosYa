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
                        .ThenInclude(o => o.Rubro)
                .Include(p => p.Ubicaciones) // include ubicaciones (direccion/lat/long)
                .FirstOrDefaultAsync(p => p.Usuario.Id == usuarioId);
        }

        public async Task<Profesional?> ObtenerPorIdAsync(int id)
        {
            return await _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                        .ThenInclude(o => o.Rubro)
                .Include(p => p.Ubicaciones) // include ubicaciones
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, double? lat, double? lng, double? maxDist, int? minimoRating)
        {
            var query = _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                        .ThenInclude(o => o.Rubro)
                .Include(p => p.Ubicaciones) // include ubicaciones
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
                double deltaLat = maxDist.Value / 111.0;
                double deltaLng = maxDist.Value / (111.0 * Math.Cos(lat.Value * Math.PI / 180.0));

                var minLat = lat.Value - deltaLat;
                var maxLat = lat.Value + deltaLat;
                var minLng = lng.Value - deltaLng;
                var maxLng = lng.Value + deltaLng;

                var profesionalesConUbicacion = _context.UbicacionesProfesionales
                    .Where(u => u.Latitud >= minLat && u.Latitud <= maxLat && 
                                u.Longitud >= minLng && u.Longitud <= maxLng)
                    .Select(u => u.ProfesionalId);

                query = query.Where(p => profesionalesConUbicacion.Contains(p.Id));
            }

            return await query.ToListAsync();
        }

        // New helper to fetch list projected to DTO-like object to avoid pulling heavy navigation properties in controllers
        public async Task<IEnumerable<OficiosYa.Application.DTOs.ProfesionalListItemDto>> ObtenerListaAsync(string? oficio, double? lat, double? lng, double? maxDist, int? minimoRating)
        {
            var query = _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                        .ThenInclude(o => o.Rubro)
                .Include(p => p.Ubicaciones)
                .AsQueryable();

            if (!string.IsNullOrEmpty(oficio))
                query = query.Where(p => p.Oficios != null && p.Oficios.Any(po => po.Oficio.Nombre.Contains(oficio)));

            if (minimoRating.HasValue)
                query = query.Where(p => p.RatingPromedio >= minimoRating.Value);

            if (lat.HasValue && lng.HasValue && maxDist.HasValue)
            {
                double deltaLat = maxDist.Value / 111.0;
                double deltaLng = maxDist.Value / (111.0 * Math.Cos(lat.Value * Math.PI / 180.0));

                var minLat = lat.Value - deltaLat;
                var maxLat = lat.Value + deltaLat;
                var minLng = lng.Value - deltaLng;
                var maxLng = lng.Value + deltaLng;

                var profesionalesConUbicacion = _context.UbicacionesProfesionales
                    .Where(u => u.Latitud >= minLat && u.Latitud <= maxLat && u.Longitud >= minLng && u.Longitud <= maxLng)
                    .Select(u => u.ProfesionalId);

                query = query.Where(p => profesionalesConUbicacion.Contains(p.Id));
            }

            // Project and prefer coordinates: use Usuario lat/long only when both are non-zero and within ranges; otherwise fallback to latest Ubicacion
            return await query.Select(p => new OficiosYa.Application.DTOs.ProfesionalListItemDto
            {
                Id = p.Id,
                Nombre = p.Usuario.Nombre,
                Apellido = p.Usuario.Apellido,
                Telefono = p.Usuario.Telefono,
                Email = p.Usuario.Email,
                Descripcion = p.Descripcion,
                RatingPromedio = p.RatingPromedio,
                TotalCalificaciones = p.TotalCalificaciones,
                FotoPerfil = p.Usuario.FotoPerfil ?? p.FotoPerfil,

                Latitud = (p.Usuario.Latitud != 0 && p.Usuario.Latitud >= -90 && p.Usuario.Latitud <= 90 && p.Usuario.Longitud != 0 && p.Usuario.Longitud >= -180 && p.Usuario.Longitud <= 180)
                            ? p.Usuario.Latitud
                            : p.Ubicaciones.OrderByDescending(u => u.UltimaActualizacion).Select(u => u.Latitud).FirstOrDefault(),

                Longitud = (p.Usuario.Latitud != 0 && p.Usuario.Latitud >= -90 && p.Usuario.Latitud <= 90 && p.Usuario.Longitud != 0 && p.Usuario.Longitud >= -180 && p.Usuario.Longitud <= 180)
                            ? p.Usuario.Longitud
                            : p.Ubicaciones.OrderByDescending(u => u.UltimaActualizacion).Select(u => u.Longitud).FirstOrDefault(),

                Direccion = (!string.IsNullOrEmpty(p.Usuario.Direccion) && p.Usuario.Direccion != "0")
                            ? p.Usuario.Direccion
                            : p.Ubicaciones.OrderByDescending(u => u.UltimaActualizacion).Select(u => u.NombreDireccion).FirstOrDefault(),

                Oficios = p.Oficios.Select(po => new OficiosYa.Application.DTOs.OficioSimpleDto
                {
                    Id = po.Oficio.Id,
                    RubroId = po.Oficio.RubroId,
                    Nombre = po.Oficio.Nombre,
                    RubroNombre = po.Oficio.Rubro != null ? po.Oficio.Rubro.Nombre : null
                }).ToList()
            }).ToListAsync();
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

        public async Task<bool> ExistsByDocumentoAsync(string documento, int? excludeProfesionalId = null)
        {
            var normalized = new string(documento.Where(char.IsDigit).ToArray());
            var query = _context.Profesionales.AsQueryable();
            if (excludeProfesionalId.HasValue)
                query = query.Where(p => p.Id != excludeProfesionalId.Value);

            return await query.AnyAsync(p => p.Documento != null && p.Documento.Replace("-", "").Replace(" ", "") == normalized);
        }

        public async Task DeleteAsync(int profesionalId)
        {
            var prof = await _context.Profesionales.FindAsync(profesionalId);
            if (prof == null) return;
            _context.Profesionales.Remove(prof);
            await _context.SaveChangesAsync();
        }
    }
}
