using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET /api/files/uploads/{filename}
        [HttpGet("uploads/{*filename}")]
        public IActionResult GetUpload(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return BadRequest();
            var uploadsRoot = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
            var filePath = Path.Combine(uploadsRoot, filename);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var provider = new PhysicalFileProvider(uploadsRoot);
            var file = provider.GetFileInfo(filename);
            if (file.PhysicalPath == null) return NotFound(); // <-- comprobación añadida

            var mime = "application/octet-stream";
            // Simple mime detection by extension
            var ext = Path.GetExtension(filename).ToLowerInvariant();
            if (ext == ".jpg" || ext == ".jpeg") mime = "image/jpeg";
            if (ext == ".png") mime = "image/png";
            if (ext == ".webp") mime = "image/webp";

            return PhysicalFile(file.PhysicalPath, mime);
        }
    }
}
