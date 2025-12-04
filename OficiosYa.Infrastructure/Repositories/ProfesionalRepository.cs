using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Domain.Services;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private readonly OficiosYaDbContext _context;
        private readonly IGeoService _geo;

        public ProfesionalRepository(OficiosYaDbContext context, IGeoService geo)
        {
            _context = context;
            _geo = geo;
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

        public async Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, int clienteId, double? maxDist, int? minimoRating)
        {
            // Obtener Ubicación 
            var ubicacionCliente = await _context.DireccionesClientes
                .FirstOrDefaultAsync(u => u.ClienteId == clienteId);

            if(ubicacionCliente == null)
            {
                throw new Exception("No se encontró la ubicación del cliente.");
            }

            double latCliente = ubicacionCliente.Latitud;
            double lonCliente = ubicacionCliente.Longitud;

            var query = _context.Profesionales
                .Include(p => p.Usuario)
                .Include(p => p.Oficios)
                    .ThenInclude(po => po.Oficio)
                .Include(p => p.Ubicaciones)
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

            var profesionales = await query.ToListAsync();

            if(!maxDist.HasValue)
            {
                return profesionales;
            }

            // Filtro por Distancia
            var filtrados = profesionales.Where(p =>
            {

                var ultimaUbicacion = p.Ubicaciones?
                .OrderByDescending(u => u.UltimaActualizacion)
                .FirstOrDefault();

                if (ultimaUbicacion == null) return false;

                double distancia = _geo.CalcularDistancia(
                    latCliente, lonCliente,
                    ultimaUbicacion.Latitud,
                    ultimaUbicacion.Longitud);

                return distancia <= maxDist.Value;
            }).ToList();
            
            return filtrados;
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
