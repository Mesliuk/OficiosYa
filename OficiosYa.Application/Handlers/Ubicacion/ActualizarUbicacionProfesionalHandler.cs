using OficiosYa.Application.Commands.Profesionales;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Ubicacion
{
    public class ActualizarUbicacionProfesionalHandler
    {
        private readonly IUbicacionRepository _ubicRepo;
        public ActualizarUbicacionProfesionalHandler(IUbicacionRepository repo) { _ubicRepo = repo; }
        
        public async Task HandleAsync(ActualizarUbicacionProfesionalCommand command)
        {
            var dto = new UbicacionProfesionalDto
            {
                ProfesionalId = command.ProfesionalId,
                Latitud = command.Latitud,
                Longitud = command.Longitud
            };

            await _ubicRepo.RegistrarUbicacionAsync(dto);
        }
    }
}
