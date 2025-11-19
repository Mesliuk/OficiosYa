using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class UsuarioService
    {
        public Task<UsuarioDto?> LoginAsync(string email, string password) => Task.FromResult<UsuarioDto?>(null);
        public Task<int> RegistrarClienteAsync(RegistroClienteDto dto) => Task.FromResult(0);
        public Task<int> RegistrarProfesionalAsync(RegistroProfesionalDto dto) => Task.FromResult(0);
    }
}
