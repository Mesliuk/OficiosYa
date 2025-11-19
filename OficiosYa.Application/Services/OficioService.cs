using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class OficioService
    {
        public Task<IEnumerable<Oficio>> ObtenerTodosAsync() => Task.FromResult<IEnumerable<Oficio>>(new List<Oficio>());
    }
}
