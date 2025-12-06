using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficios
{
    public class AssignOficioToProfesionalHandler
    {
        private readonly IProfesionalRepository _profRepo;
        private readonly IOficioRepository _oficioRepo;

        public AssignOficioToProfesionalHandler(IProfesionalRepository profRepo, IOficioRepository oficioRepo)
        {
            _profRepo = profRepo;
            _oficioRepo = oficioRepo;
        }

        public async Task HandleAsync(int profesionalId, int oficioId)
        {
            // 1. Obtener profesional (incluyendo la colección de Oficios)
            var profesional = await _profRepo.ObtenerPorIdAsync(profesionalId);
            if (profesional == null)
                throw new InvalidOperationException($"Profesional con id {profesionalId} no encontrado.");

            // 2. Obtener oficio
            var oficio = await _oficioRepo.ObtenerPorIdAsync(oficioId);
            if (oficio == null)
                throw new InvalidOperationException($"Oficio con id {oficioId} no encontrado.");

            // 3. Verificar si ya está asignado
            if (profesional.Oficios.Any(po => po.OficioId == oficioId))
                throw new InvalidOperationException("El oficio ya está asignado al profesional.");

            // 4. Agregar relación
            var profOficio = new ProfesionalOficio
            {
                ProfesionalId = profesionalId,
                OficioId = oficioId
            };

            profesional.Oficios.Add(profOficio);

            // 5. Guardar cambios mediante el repositorio (unit of work dentro del repo)
            await _profRepo.UpdateAsync(profesional);
        }
    }
}

