using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Handlers.Profesional;
using OficiosYa.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using OficiosYa.Application.Interfaces;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private readonly GetProfesionalByIdHandler _getHandler;
        private readonly UpdateProfesionalHandler _updateHandler;
        private readonly SearchProfesionalHandler _searchHandler;
        private readonly IProfesionalRepository _profRepo;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        private static readonly string[] AllowedImageMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };

        public ProfesionalController(
            GetProfesionalByIdHandler getHandler,
            UpdateProfesionalHandler updateHandler,
            SearchProfesionalHandler searchHandler,
            IProfesionalRepository profRepo,
            IConfiguration config,
            IWebHostEnvironment env)
        {
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _searchHandler = searchHandler;
            _profRepo = profRepo;
            _config = config;
            _env = env;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var profesional = await _getHandler.HandleAsync(usuarioId);
            if (profesional == null) return NotFound();
            return Ok(profesional);
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody] UpdateProfesionalRequest request)
        {
            var profesional = await _getHandler.HandleAsync(usuarioId);
            if (profesional == null) return NotFound();

            // Update fields
            profesional.Documento = request.Documento;
            // Update Usuario fields if needed, e.g. FotoPerfil
            if (profesional.Usuario != null)
            {
                profesional.Usuario.Nombre = request.Nombre;
                profesional.Usuario.Apellido = request.Apellido;
                profesional.Usuario.Telefono = request.Telefono;
            }

            await _updateHandler.HandleAsync(profesional);
            return NoContent();
        }

        // PUT api/profesional/{usuarioId}/foto
        [HttpPut("{usuarioId}/foto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePhoto(int usuarioId, IFormFile FotoPerfil)
        {
            if (FotoPerfil == null) return BadRequest("No file provided");
            if (FotoPerfil.Length > 2_000_000) return BadRequest("FotoPerfil too large (max 2MB)");
            if (!AllowedImageMimeTypes.Contains(FotoPerfil.ContentType)) return BadRequest("Unsupported image type. Allowed: jpeg, png, webp");

            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            // remove old file if exists
            if (!string.IsNullOrEmpty(profesional.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", profesional.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
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

            profesional.FotoPerfil = relativePath;
            await _profRepo.UpdateAsync(profesional);

            return Ok(new { FotoPerfil = relativePath });
        }

        // DELETE api/profesional/{usuarioId}/foto
        [HttpDelete("{usuarioId}/foto")]
        public async Task<IActionResult> DeletePhoto(int usuarioId)
        {
            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            if (!string.IsNullOrEmpty(profesional.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", profesional.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
                try { if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile); } catch { }
                profesional.FotoPerfil = null;
                await _profRepo.UpdateAsync(profesional);
            }

            return NoContent();
        }
    }

    public class UpdateProfesionalRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
    }
}
