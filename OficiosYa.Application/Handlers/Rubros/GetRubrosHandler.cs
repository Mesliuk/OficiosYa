using OficiosYa.Application.Interfaces;
using OficiosYa.Application.DTOs;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Rubros
{
    public class GetRubrosHandler
    {
        private readonly IRubroRepository _repo;

        public GetRubrosHandler(IRubroRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RubroDto>> HandleAsync(string? search = null, int page = 1, int size = 100)
        {
            var rubros = await _repo.ObtenerTodosConOficiosAsync();

            // map
            var list = rubros.Select(r => new RubroDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Slug = r.Slug,
                Descripcion = r.Descripcion,
                Oficios = r.Oficios
                            .Where(o => string.IsNullOrWhiteSpace(search) || o.Nombre.ToLower().Contains(search.ToLower()))
                            .OrderBy(o => o.Id)
                            .Skip((page - 1) * size)
                            .Take(size)
                            .Select(o => new OficioDto
                            {
                                Id = o.Id,
                                RubroId = o.RubroId,
                                Nombre = o.Nombre,
                                Descripcion = o.Descripcion,
                                RequiereLicencia = o.RequiereLicencia,
                                Activo = o.Activo
                            }).ToList()
            }).ToList();

            return list;
        }

        public async Task<RubroDto?> HandleByIdAsync(int id)
        {
            var r = await _repo.ObtenerPorIdConOficiosAsync(id);
            if (r == null) return null;

            return new RubroDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Slug = r.Slug,
                Descripcion = r.Descripcion,
                Oficios = r.Oficios.OrderBy(o => o.Id).Select(o => new OficioDto
                {
                    Id = o.Id,
                    RubroId = o.RubroId,
                    Nombre = o.Nombre,
                    Descripcion = o.Descripcion,
                    RequiereLicencia = o.RequiereLicencia,
                    Activo = o.Activo
                }).ToList()
            };
        }
    }
}
