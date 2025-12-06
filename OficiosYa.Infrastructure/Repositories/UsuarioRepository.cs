using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using OficiosYa.Domain.Enums;
using OficiosYa.Infrastructure.Persistence;

namespace OficiosYa.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly OficiosYaDbContext _context;

        public UsuarioRepository(OficiosYaDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            var e = email?.Trim() ?? string.Empty;
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, e));
        }

        public async Task<Usuario?> ObtenerPorEmailYRolAsync(string email, UsuarioRoleEnum rol)
        {
            var e = email?.Trim() ?? string.Empty;
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, e) && u.Rol == rol);
        }

        public async Task AgregarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string passwordHash)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return false;

            usuario.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
