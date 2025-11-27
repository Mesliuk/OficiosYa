using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public DireccionClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET api/direccioncliente/{usuarioId}
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetDirecciones(int usuarioId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var dirs = cliente.Direcciones.Select(d => new {
                d.Id,
                d.Alias,
                d.Direccion,
                d.Latitud,
                d.Longitud
            });

            return Ok(dirs);
        }

        // POST api/direccioncliente/{usuarioId}
        [HttpPost("{usuarioId}")]
        public async Task<IActionResult> AddDireccion(int usuarioId, [FromBody] CreateDireccionRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var direccion = new DireccionCliente
            {
                Alias = string.IsNullOrWhiteSpace(request.Alias) ? "Ubicación" : request.Alias,
                Direccion = request.Direccion,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                ClienteId = cliente.Id
            };

            cliente.Direcciones.Add(direccion);
            await _clienteRepository.ActualizarAsync(cliente);

            return CreatedAtAction(nameof(GetDirecciones), new { usuarioId = usuarioId }, direccion);
        }

        // PUT api/direccioncliente/{usuarioId}/{direccionId}
        [HttpPut("{usuarioId}/{direccionId}")]
        public async Task<IActionResult> UpdateDireccion(int usuarioId, int direccionId, [FromBody] UpdateDireccionRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var direccion = cliente.Direcciones.FirstOrDefault(d => d.Id == direccionId);
            if (direccion == null) return NotFound();

            direccion.Alias = request.Alias ?? direccion.Alias;
            direccion.Direccion = request.Direccion ?? direccion.Direccion;
            direccion.Latitud = request.Latitud ?? direccion.Latitud;
            direccion.Longitud = request.Longitud ?? direccion.Longitud;

            await _clienteRepository.ActualizarAsync(cliente);
            return NoContent();
        }

        // DELETE api/direccioncliente/{usuarioId}/{direccionId}
        [HttpDelete("{usuarioId}/{direccionId}")]
        public async Task<IActionResult> DeleteDireccion(int usuarioId, int direccionId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var direccion = cliente.Direcciones.FirstOrDefault(d => d.Id == direccionId);
            if (direccion == null) return NotFound();

            cliente.Direcciones.Remove(direccion);
            await _clienteRepository.ActualizarAsync(cliente);

            return NoContent();
        }
    }

    public class CreateDireccionRequest
    {
        public string Alias { get; set; } = "Ubicación";
        public string Direccion { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }

    public class UpdateDireccionRequest
    {
        public string? Alias { get; set; }
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
    }
}
