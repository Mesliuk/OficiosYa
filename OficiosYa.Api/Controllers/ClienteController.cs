using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Handlers.Cliente;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UpdateClienteHandler _updateClienteHandler;

        public ClienteController(IClienteRepository clienteRepository, UpdateClienteHandler updateClienteHandler)
        {
            _clienteRepository = clienteRepository;
            _updateClienteHandler = updateClienteHandler;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null)
                return NotFound();

            return Ok(new { 
                cliente.Id,
                cliente.Usuario.Nombre,
                cliente.Usuario.Apellido,
                cliente.Usuario.Telefono,
                cliente.Usuario.Email
              
            });
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody] UpdateClienteRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null)
                return NotFound();

            // Update fields in the related Usuario entity
            if (cliente.Usuario != null)
            {
                cliente.Usuario.Nombre = request.Nombre;
                cliente.Usuario.Apellido = request.Apellido;
                cliente.Usuario.Telefono = request.Telefono;
                cliente.Usuario.FotoPerfil = request.FotoPerfil;
            }

            // Call handler
            var result = await _updateClienteHandler.HandleAsync(cliente);
            return Ok(result);
        }
    }

    public class UpdateClienteRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? FotoPerfil { get; set; }
    }
}
