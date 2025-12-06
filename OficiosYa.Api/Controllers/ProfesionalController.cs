using Microsoft.AspNetCore.Mvc;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using OficiosYa.Application.Handlers.Usuarios;
using OficiosYa.Api.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private static readonly string[] AllowedImageMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };

        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        private readonly RegisterProfesionalHandler _registerHandler;
        private readonly IProfesionalRepository _profRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IOficioRepository _oficioRepo;

        public ProfesionalController(
            RegisterProfesionalHandler registerHandler,
            IProfesionalRepository profRepo,
            IUsuarioRepository usuarioRepo,
            IOficioRepository oficioRepo,
            IConfiguration config,
            IWebHostEnvironment env)
        {
            _registerHandler = registerHandler;
            _profRepo = profRepo;
            _usuarioRepo = usuarioRepo;
            _oficioRepo = oficioRepo;
            _config = config;
            _env = env;
        }

        // GET api/profesional/{usuarioId}
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            // Map entity to a safe DTO/anonym object to avoid circular references and null entries
            var profesionalDto = new
            {
                profesional.Id,
                profesional.Documento,
                profesional.Verificado,
                profesional.RatingPromedio,
                profesional.TotalCalificaciones,
                profesional.Descripcion,
                Usuario = profesional.Usuario == null ? null : new
                {
                    profesional.Usuario.Id,
                    profesional.Usuario.Nombre,
                    profesional.Usuario.Apellido,
                    profesional.Usuario.Email,
                    profesional.Usuario.Telefono,
                    profesional.Usuario.Direccion,
                    profesional.Usuario.Latitud,
                    profesional.Usuario.Longitud,
                    FotoPerfil = profesional.Usuario.FotoPerfil,
                    Rol = profesional.Usuario.Rol.ToString()
                },
                Oficios = profesional.Oficios?.Select(po => new
                {
                    po.Id,
                    po.ProfesionalId,
                    po.OficioId,
                    po.AnosExperiencia,
                    Oficio = po.Oficio == null ? null : new
                    {
                        po.Oficio.Id,
                        po.Oficio.RubroId,
                        po.Oficio.Nombre,
                        po.Oficio.Descripcion,
                        po.Oficio.RequiereLicencia,
                        po.Oficio.Activo,
                        Rubro = po.Oficio.Rubro == null ? null : new
                        {
                            po.Oficio.Rubro.Id,
                            po.Oficio.Rubro.Nombre,
                            po.Oficio.Rubro.Slug,
                            po.Oficio.Rubro.Descripcion
                        }
                    }
                }).ToList()
            };

            return Ok(new { Profesional = profesionalDto });
        }

        // POST api/profesional -> register a new profesional (creates usuario + profesional)
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] RegisterProfesionalRequest request, IFormFile? FotoPerfil = null)
        {
            string? fotoPath = null;
            if (FotoPerfil != null)
            {
                if (FotoPerfil.Length > 2_000_000) return BadRequest("FotoPerfil too large (max 2MB)");
                if (!AllowedImageMimeTypes.Contains(FotoPerfil.ContentType)) return BadRequest("Unsupported image type. Allowed: jpeg, png, webp");

                var uploadsRoot = _config.GetValue<string>("Uploads:RootPath") ?? Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsRoot);
                var fileName = Path.GetRandomFileName() + Path.GetExtension(FotoPerfil.FileName);
                var filePath = Path.Combine(uploadsRoot, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await FotoPerfil.CopyToAsync(stream);
                }
                fotoPath = Path.Combine("uploads", fileName).Replace('\\','/');
            }
            // map API model to application DTO
            var dto = new RegistroProfesionalDto
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Correo,
                Telefono = request.Telefono,
                Password = request.Password,
                Documento = request.Documento,
                OficiosIds = request.OficiosIds ?? new List<int>(),
                Direccion = request.Direccion,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                Descripcion = request.Descripcion,
                FotoPerfil = fotoPath
            };

            var result = await _registerHandler.HandleAsync(dto);
            return CreatedAtAction(nameof(GetByUsuarioId), new { usuarioId = result.Id }, result);
        }

        // PUT api/profesional/{usuarioId}
        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody] UpdateProfesionalRequest request)
        {
            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            profesional.Documento = request.Documento;
            profesional.Descripcion = request.Descripcion ?? profesional.Descripcion;
            profesional.Verificado = request.Verificado ?? profesional.Verificado;
            profesional.RatingPromedio = request.RatingPromedio ?? profesional.RatingPromedio;
            profesional.TotalCalificaciones = request.TotalCalificaciones ?? profesional.TotalCalificaciones;

            if (profesional.Usuario != null)
            {
                profesional.Usuario.Nombre = request.Nombre;
                profesional.Usuario.Apellido = request.Apellido;
                profesional.Usuario.Telefono = request.Telefono;
                if (!string.IsNullOrEmpty(request.UsuarioEmail)) profesional.Usuario.Email = request.UsuarioEmail;
            }

            if (request.OficiosIds != null)
            {
                profesional.Oficios.Clear();
                foreach (var id in request.OficiosIds)
                {
                    var oficio = await _oficioRepo.ObtenerPorIdAsync(id);
                    if (oficio != null)
                    {
                        profesional.Oficios.Add(new ProfesionalOficio { Oficio = oficio, OficioId = id });
                    }
                }
            }

            await _profRepo.UpdateAsync(profesional);

            return NoContent();
        }

        // DELETE api/profesional/{usuarioId}?onlyLocation=true
        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Delete(int usuarioId, [FromQuery] bool onlyLocation = false)
        {
            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            if (onlyLocation)
            {
                return BadRequest("Location management removed");
            }

            await _profRepo.DeleteAsync(profesional.Id);

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

            // remove old file if exists - foto now stored in Usuario.FotoPerfil
            if (!string.IsNullOrEmpty(profesional.Usuario?.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", profesional.Usuario.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
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

            if (profesional.Usuario != null)
            {
                profesional.Usuario.FotoPerfil = relativePath;
                await _profRepo.UpdateAsync(profesional);
            }

            return Ok(new { FotoPerfil = relativePath });
        }

        // DELETE api/profesional/{usuarioId}/foto
        [HttpDelete("{usuarioId}/foto")]
        public async Task<IActionResult> DeletePhoto(int usuarioId)
        {
            var profesional = await _profRepo.GetByUsuarioIdAsync(usuarioId);
            if (profesional == null) return NotFound();

            if (!string.IsNullOrEmpty(profesional.Usuario?.FotoPerfil))
            {
                var oldFile = Path.Combine(_env.ContentRootPath, "wwwroot", profesional.Usuario.FotoPerfil.Replace('/', Path.DirectorySeparatorChar));
                try { if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile); } catch { }
                if (profesional.Usuario != null)
                {
                    profesional.Usuario.FotoPerfil = null;
                    await _profRepo.UpdateAsync(profesional);
                }
            }

            return NoContent();
        }
    }
 }
