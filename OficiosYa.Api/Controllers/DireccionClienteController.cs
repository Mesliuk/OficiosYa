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
                d.Descripcion,
                d.Direccion,
                d.Latitud,
                d.Longitud,
                d.EsPrincipal
            });

            return Ok(dirs);
        }

        // POST api/direccioncliente/{usuarioId}
        [HttpPost("{usuarioId}")]
        public async Task<IActionResult> AddDireccion(int usuarioId, [FromBody] CreateDireccionRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            // If new address is marked as principal, clear existing principal flags
            if (request.EsPrincipal)
            {
                foreach (var d in cliente.Direcciones)
                    d.EsPrincipal = false;
            }

            var direccion = new DireccionCliente
            {
                Descripcion = string.IsNullOrWhiteSpace(request.Descripcion) ? "Ubicación" : request.Descripcion,
                Direccion = request.Direccion,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                ClienteId = cliente.Id,
                EsPrincipal = request.EsPrincipal
            };

            cliente.Direcciones.Add(direccion);
            await _clienteRepository.ActualizarAsync(cliente);

            return CreatedAtAction(nameof(GetDirecciones), new { usuarioId = usuarioId }, new {
                direccion.Id,
                direccion.Descripcion,
                direccion.Direccion,
                direccion.Latitud,
                direccion.Longitud,
                direccion.EsPrincipal
            });
        }

        // PUT api/direccioncliente/{usuarioId}/{direccionId}
        [HttpPut("{usuarioId}/{direccionId}")]
        public async Task<IActionResult> UpdateDireccion(int usuarioId, int direccionId, [FromBody] UpdateDireccionRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var direccion = cliente.Direcciones.FirstOrDefault(d => d.Id == direccionId);
            if (direccion == null) return NotFound();

            direccion.Descripcion = request.Descripcion ?? direccion.Descripcion;
            direccion.Direccion = request.Direccion ?? direccion.Direccion;
            direccion.Latitud = request.Latitud ?? direccion.Latitud;
            direccion.Longitud = request.Longitud ?? direccion.Longitud;

            if (request.EsPrincipal.HasValue)
            {
                if (request.EsPrincipal.Value)
                {
                    // unset other principals
                    foreach (var d in cliente.Direcciones)
                        d.EsPrincipal = false;
                    direccion.EsPrincipal = true;
                }
                else
                {
                    // unmark this as principal
                    direccion.EsPrincipal = false;
                }
            }

            await _clienteRepository.ActualizarAsync(cliente);
            return NoContent();
        }

        // PUT api/direccioncliente/{usuarioId}/{direccionId}/principal
        [HttpPut("{usuarioId}/{direccionId}/principal")]
        public async Task<IActionResult> SetPrincipal(int usuarioId, int direccionId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            var direccion = cliente.Direcciones.FirstOrDefault(d => d.Id == direccionId);
            if (direccion == null) return NotFound();

            // unset other principals
            foreach (var d in cliente.Direcciones)
                d.EsPrincipal = false;

            direccion.EsPrincipal = true;

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

            // If deleted was principal, optionally set another as principal (choose first)
            if (direccion.EsPrincipal && cliente.Direcciones.Any())
            {
                cliente.Direcciones.First().EsPrincipal = true;
            }

            await _clienteRepository.ActualizarAsync(cliente);

            return NoContent();
        }
    }

    public class CreateDireccionRequest
    {
        public string Descripcion { get; set; } = "Ubicación";
        public string Direccion { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool EsPrincipal { get; set; } = false;
    }

    public class UpdateDireccionRequest
    {
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public bool? EsPrincipal { get; set; }
    }
}
