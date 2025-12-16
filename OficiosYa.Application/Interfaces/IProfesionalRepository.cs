using OficiosYa.Domain.Entities;
using OficiosYa.Application.DTOs;

namespace OficiosYa.Application.Interfaces
{
    public interface IProfesionalRepository
    {
        Task<Profesional?> GetByUsuarioIdAsync(int usuarioId);
        Task<Profesional?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, double? lat, double? lng, double? maxDist, int? minimoRating);
        Task AgregarAsync(Profesional profesional);
        Task UpdateAsync(Profesional profesional);
        Task<bool> ExistsByDocumentoAsync(string documento, int? excludeProfesionalId = null);
        Task DeleteAsync(int profesionalId);
        Task ActualizarRatingAsync(int receptorId, double v1, int v2);
    }
}
