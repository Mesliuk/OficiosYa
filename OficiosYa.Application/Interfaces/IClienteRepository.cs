using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByUsuarioIdAsync(int usuarioId);
        Task ActualizarAsync(Cliente cliente);
        Task AgregarAsync(Cliente cliente);
    }
}
