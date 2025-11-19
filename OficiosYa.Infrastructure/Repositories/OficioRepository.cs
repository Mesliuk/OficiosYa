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

        public async Task<IEnumerable<Oficio>> GetAllAsync()
        {
            return await _context.Oficios.ToListAsync();
        }

        public async Task CreateAsync(Oficio oficio)
        {
            _context.Oficios.Add(oficio);
            await _context.SaveChangesAsync();
        }
    }
}
