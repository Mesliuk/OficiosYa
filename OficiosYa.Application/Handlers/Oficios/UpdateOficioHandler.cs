using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficios
{
    public class UpdateOficioHandler
    {
        private readonly IOficioRepository _repository;

        public UpdateOficioHandler(IOficioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(int id, string nombre, string descripcion)
        {
            var oficio = await _repository.ObtenerPorIdAsync(id);
            if (oficio != null)
            {
                oficio.Nombre = nombre;
                oficio.Descripcion = descripcion;
                await _repository.ActualizarAsync(oficio);
                return true;
            }

            return false;
        }
    }
}
