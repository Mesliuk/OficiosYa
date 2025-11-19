using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class ProfesionalService
    {
        public Task<Profesional?> ObtenerPorUsuarioAsync(int usuarioId) => Task.FromResult<Profesional?>(null);
        public Task<IEnumerable<Profesional>> BuscarAsync(string? oficio, double? lat, double? lng, double? maxDist, int? ratingMin)
        => Task.FromResult<IEnumerable<Profesional>>(new List<Profesional>());
    }
}
