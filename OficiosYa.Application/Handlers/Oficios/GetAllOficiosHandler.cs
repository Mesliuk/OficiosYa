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

        public async Task<IEnumerable<Oficio>> HandleAsync(int? rubroId = null, string? search = null)
        {
            if (rubroId.HasValue) return await _repository.ObtenerPorRubroAsync(rubroId.Value);
            if (!string.IsNullOrWhiteSpace(search)) return await _repository.BuscarAsync(search);
            return await _repository.ObtenerTodosAsync();
        }
    }
}
