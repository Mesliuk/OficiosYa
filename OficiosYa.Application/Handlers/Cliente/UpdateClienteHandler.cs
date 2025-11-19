using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Cliente
{
    public class UpdateClienteHandler
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IUsuarioRepository _usuarioRepo;


        public UpdateClienteHandler(IClienteRepository clienteRepo, IUsuarioRepository usuarioRepo)
        {
            _clienteRepo = clienteRepo;
            _usuarioRepo = usuarioRepo;
        }


        public async Task<UsuarioDto> HandleAsync(OficiosYa.Domain.Entities.Cliente cliente)
        {
            // Ejemplo de actualización: se puede modificar nombre, apellido, telefono
            await _clienteRepo.ActualizarAsync(cliente);


            var usuario = cliente.Usuario;
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString()
            };
        }
    }
}
