using OficiosYa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class CalificacionService
    {
        public Task RegistrarAsync(CalificacionDto dto) => Task.CompletedTask;
        public Task<IEnumerable<CalificacionDto>> ObtenerPorUsuarioAsync(int usuarioId)
        => Task.FromResult<IEnumerable<CalificacionDto>>(new List<CalificacionDto>());
    }
}
