using System;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IOficioRepository
    {
        Task<IEnumerable<Oficio>> ObtenerTodosAsync();
        Task<IEnumerable<Oficio>> ObtenerPorRubroAsync(int rubroId);
        Task<IEnumerable<Oficio>> BuscarAsync(string term);
        Task<Oficio?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Oficio oficio);
        Task ActualizarAsync(Oficio oficio);
        Task EliminarAsync(int id);
    }
}
