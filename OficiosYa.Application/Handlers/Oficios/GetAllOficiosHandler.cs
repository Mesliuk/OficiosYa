using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficios
{
    public class GetAllOficiosHandler
    {
        private readonly IOficioRepository _repository;

        public GetAllOficiosHandler(IOficioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Oficio>> HandleAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }
    }
}
