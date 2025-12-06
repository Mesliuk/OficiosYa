using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class OficioRepository : IOficioRepository
    {
        private readonly OficiosYaDbContext _context;

        public OficioRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Oficio>> ObtenerTodosAsync()
        {
            return await _context.Oficios.Include(o => o.Rubro).ToListAsync();
        }

        public async Task<IEnumerable<Oficio>> ObtenerPorRubroAsync(int rubroId)
        {
            return await _context.Oficios.Where(o => o.RubroId == rubroId).Include(o => o.Rubro).ToListAsync();
        }

        public async Task<IEnumerable<Oficio>> BuscarAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return await ObtenerTodosAsync();
            var t = term.Trim();
            return await _context.Oficios
                .Where(o => EF.Functions.ILike(o.Nombre, $"%{t}%") || EF.Functions.ILike(o.Descripcion, $"%{t}%"))
                .Include(o => o.Rubro)
                .ToListAsync();
        }

        public async Task<Oficio?> ObtenerPorIdAsync(int id)
        {
            return await _context.Oficios.Include(o => o.Rubro).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AgregarAsync(Oficio oficio)
        {
            _context.Oficios.Add(oficio);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Oficio oficio)
        {
            _context.Oficios.Update(oficio);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var oficio = await _context.Oficios.FindAsync(id);
            if (oficio != null)
            {
                _context.Oficios.Remove(oficio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
