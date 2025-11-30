using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficios
{
    public class CreateOficioHandler
    {
        private readonly IOficioRepository _repository;

        public CreateOficioHandler(IOficioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Oficio> HandleAsync(string nombre, string descripcion)
        {
            var oficio = new Oficio
            {
                Nombre = nombre,
                Descripcion = descripcion
            };
            await _repository.AgregarAsync(oficio);
            return oficio;
        }
    }
}
