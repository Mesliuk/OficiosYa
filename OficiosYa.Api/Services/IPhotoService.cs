using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OficiosYa.Api.Services
{
    public interface IPhotoService
    {
        Task<string> SaveAsync(IFormFile file);
        Task DeleteAsync(string? relativePath);
    }
}
