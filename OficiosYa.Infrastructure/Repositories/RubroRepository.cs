using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OficiosYa.Infrastructure.Repositories
{
    public class RubroRepository : IRubroRepository
    {
        private readonly OficiosYaDbContext _context;

        public RubroRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rubro>> ObtenerTodosConOficiosAsync()
        {
            return await _context.Rubros
                .Include(r => r.Oficios)
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<Rubro?> ObtenerPorIdConOficiosAsync(int id)
        {
            return await _context.Rubros
                .Include(r => r.Oficios)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
