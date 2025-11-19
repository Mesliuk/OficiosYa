using OficiosYa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task<UsuarioDto?> GetByEmailAsync(string email);
        Task<int> CreateAsync(RegistroClienteDto dto);
        Task<int> CreateProfesionalAsync(RegistroProfesionalDto dto);
        Task<bool> UpdatePasswordAsync(int userId, string passwordHash);
    }
}
