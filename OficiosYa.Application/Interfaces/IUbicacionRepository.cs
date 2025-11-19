using OficiosYa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IUbicacionRepository
    {
        Task RegistrarUbicacionAsync(UbicacionProfesionalDto dto);
        Task<UbicacionProfesionalDto?> GetByProfesionalAsync(int profesionalId);
    }
}
