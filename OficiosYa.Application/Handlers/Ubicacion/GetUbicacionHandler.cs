using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Ubicacion
{
    public class GetUbicacionHandler
    {
        private readonly IUbicacionRepository _repository;

        public GetUbicacionHandler(IUbicacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<UbicacionProfesionalDto?> HandleAsync(int profesionalId)
        {
            return await _repository.GetByProfesionalAsync(profesionalId);
        }
    }
}
