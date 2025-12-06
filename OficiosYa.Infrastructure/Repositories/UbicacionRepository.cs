using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using System.Threading.Tasks;

namespace OficiosYa.Infrastructure.Repositories
{
    // No-op implementation kept to satisfy DI after removing full location feature
    public class UbicacionRepository : IUbicacionRepository
    {
        public Task RegistrarUbicacionAsync(UbicacionProfesionalDto dto)
        {
            // intentionally no-op
            return Task.CompletedTask;
        }

        public Task<UbicacionProfesionalDto?> GetByProfesionalAsync(int profesionalId)
        {
            // always return null since location feature removed
            return Task.FromResult<UbicacionProfesionalDto?>(null);
        }

        public Task DeleteByProfesionalAsync(int profesionalId)
        {
            // no-op
            return Task.CompletedTask;
        }
    }
}
