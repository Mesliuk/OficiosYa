using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class UbicacionService
    {
        public Task RegistrarAsync(UbicacionProfesionalDto dto) => Task.CompletedTask;
        public Task<UbicacionProfesionalDto?> ObtenerAsync(int profesionalId)
        => Task.FromResult<UbicacionProfesionalDto?>(null);
    }
}
