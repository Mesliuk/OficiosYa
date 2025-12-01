using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IRubroRepository
    {
        Task<IEnumerable<Rubro>> ObtenerTodosConOficiosAsync();
        Task<Rubro?> ObtenerPorIdConOficiosAsync(int id);
    }
}
