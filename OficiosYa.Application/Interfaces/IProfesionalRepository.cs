using System;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IProfesionalRepository
    {
        Task<Profesional?> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Profesional>> BuscarPorFiltrosAsync(string? oficio, double? lat, double? lng, double? maxDist, int? minimoRating);
        Task AgregarAsync(Profesional profesional);
        Task UpdateAsync(Profesional profesional);
    }
}
