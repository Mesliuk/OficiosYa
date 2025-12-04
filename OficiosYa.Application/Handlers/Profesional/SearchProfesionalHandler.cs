using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Profesional
{
    public class SearchProfesionalHandler
    {
        private readonly IProfesionalRepository _repository;

        public SearchProfesionalHandler(IProfesionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OficiosYa.Domain.Entities.Profesional>> HandleAsync(string? oficio, int clienteId, double? maxDist, int? minimoRating)
        {
            return await _repository.BuscarPorFiltrosAsync(oficio, clienteId, maxDist, minimoRating);
        }
    }
}
