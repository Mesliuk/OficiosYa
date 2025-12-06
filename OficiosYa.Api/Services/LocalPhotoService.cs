using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OficiosYa.Api.Services
{
    public class LocalPhotoService : IPhotoService
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowed = new[] { "image/jpeg", "image/png", "image/webp" };
        private const long MaxSize = 2_000_000;

        public LocalPhotoService(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public async Task<string> SaveAsync(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (file.Length == 0) throw new ArgumentException("File is empty", nameof(file));
            if (file.Length > MaxSize) throw new ArgumentException("File too large", nameof(file));
            if (Array.IndexOf(_allowed, file.ContentType) < 0) throw new ArgumentException("Unsupported content type", nameof(file));

            var uploadsRoot = _config.GetValue<string>("Uploads:RootPath") ?? Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsRoot);
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsRoot, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("uploads", fileName).Replace('\\','/');
            return relativePath;
        }

        public Task DeleteAsync(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return Task.CompletedTask;
            var physical = Path.Combine(_env.ContentRootPath, "wwwroot", relativePath.Replace('/', Path.DirectorySeparatorChar));
            try { if (System.IO.File.Exists(physical)) System.IO.File.Delete(physical); } catch { }
            return Task.CompletedTask;
        }
    }
}
