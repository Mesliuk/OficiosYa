using OficiosYa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IClasificacionRepository
    {
        Task RegistrarAsync(CalificacionDto dto);
        Task<IEnumerable<CalificacionDto>> GetByReceptorAsync(int receptorId);
        Task<double> ObtenerPromedioAsync(int receptorId);
        Task<IList<CalificacionDto>> ObtenerPorReceptorAsync(int receptorId);
    }
}
