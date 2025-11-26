using OficiosYa.Application.Interfaces;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficios
{
    public class DeleteOficioHandler
    {
        private readonly IOficioRepository _repository;

        public DeleteOficioHandler(IOficioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(int id)
        {
            var oficio = await _repository.ObtenerPorIdAsync(id);
            if (oficio == null)
                return false;

            await _repository.EliminarAsync(id);
            return true;
        }
    }
}
