using OficiosYa.Domain.Entities;

namespace OficiosYa.Application.Interfaces
{
    public interface IProfesionalRepository
    {
        Task<Profesional?> GetByUsuarioIdAsync(int usuarioId);
        Task<Profesional?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, int clienteId, double? maxDist, int? minimoRating);
        Task AgregarAsync(Profesional profesional);
        Task UpdateAsync(Profesional profesional);
    }
}
