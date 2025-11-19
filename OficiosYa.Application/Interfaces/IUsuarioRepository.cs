using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task AgregarAsync(Usuario usuario);
        Task<Usuario?> GetByIdAsync(int id);
        Task<bool> UpdatePasswordAsync(int userId, string passwordHash);
    }
}
