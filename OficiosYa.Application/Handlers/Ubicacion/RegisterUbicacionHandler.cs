using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Ubicacion
{
    public class RegisterUbicacionHandler
    {
        private readonly IUbicacionRepository _repository;

        public RegisterUbicacionHandler(IUbicacionRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(UbicacionProfesionalDto dto)
        {
            // Location feature removed: no-op handler
            return Task.CompletedTask;
        }
    }
}
