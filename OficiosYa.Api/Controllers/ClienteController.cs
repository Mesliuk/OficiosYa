using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Handlers.Cliente;
using OficiosYa.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UpdateClienteHandler _updateClienteHandler;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        private static readonly string[] AllowedImageMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };

        public ClienteController(IClienteRepository clienteRepository, UpdateClienteHandler updateClienteHandler, IConfiguration config, IWebHostEnvironment env)
        {
            _clienteRepository = clienteRepository;
            _updateClienteHandler = updateClienteHandler;
            _config = config;
            _env = env;
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
                cliente.Usuario.Email,
                cliente.Usuario.Direccion,
                cliente.Usuario.Latitud,
                cliente.Usuario.Longitud,
                FotoPerfil = cliente.FotoPerfil
            });
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody] UpdateClienteRequest request)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null)
                return NotFound();

            if (cliente.Usuario != null)
            {
                cliente.Usuario.Nombre = request.Nombre;
                cliente.Usuario.Apellido = request.Apellido;
                cliente.Usuario.Telefono = request.Telefono;
                
                if (request.Direccion != null)
                    cliente.Usuario.Direccion = request.Direccion;
                
                if (request.Latitud.HasValue)
                    cliente.Usuario.Latitud = request.Latitud.Value;
                
                if (request.Longitud.HasValue)
                    cliente.Usuario.Longitud = request.Longitud.Value;
            }

            var result = await _updateClienteHandler.HandleAsync(cliente);
            return Ok(result);
        }

        [HttpPut("{usuarioId}/foto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePhoto(int usuarioId, IFormFile FotoPerfil)
        {
            if (FotoPerfil == null) return BadRequest("No file provided");
            if (FotoPerfil.Length > 2_000_000) return BadRequest("FotoPerfil too large (max 2MB)");
            if (!AllowedImageMimeTypes.Contains(FotoPerfil.ContentType)) return BadRequest("Unsupported image type. Allowed: jpeg, png, webp");

            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            if (!string.IsNullOrEmpty(cliente.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", cliente.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
                try { if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile); } catch { }
            }

            var uploadsRoot = _config.GetValue<string>("Uploads:RootPath") ?? Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsRoot);
            var fileName = Path.GetRandomFileName() + Path.GetExtension(FotoPerfil.FileName);
            var filePath = Path.Combine(uploadsRoot, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await FotoPerfil.CopyToAsync(stream);
            }
            var relativePath = Path.Combine("uploads", fileName).Replace('\\','/');

            cliente.FotoPerfil = relativePath;
            await _clienteRepository.ActualizarAsync(cliente);

            return Ok(new { FotoPerfil = relativePath });
        }

        [HttpDelete("{usuarioId}/foto")]
        public async Task<IActionResult> DeletePhoto(int usuarioId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null) return NotFound();

            if (!string.IsNullOrEmpty(cliente.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", cliente.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
                try { if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile); } catch { }
                cliente.FotoPerfil = null;
                await _clienteRepository.ActualizarAsync(cliente);
            }

            return NoContent();
        }
    }

    public class UpdateClienteRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
    }
}
