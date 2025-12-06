using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Profesional
{
    public class GetProfesionalByIdHandler
    {
        private readonly IProfesionalRepository _repository;

        public GetProfesionalByIdHandler(IProfesionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<OficiosYa.Domain.Entities.Profesional?> HandleAsync(int usuarioId)
        {
            return await _repository.GetByUsuarioIdAsync(usuarioId);
        }
    }
}
