using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Infrastructure.Persistence;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly OficiosYaDbContext _context;

        public ClienteRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Clientes
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }

        public async Task ActualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
