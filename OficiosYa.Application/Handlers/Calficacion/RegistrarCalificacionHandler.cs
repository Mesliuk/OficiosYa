using OficiosYa.Application.Commands.Calificaciones;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Calficacion
{
    public class RegistrarCalificacionHandler
    {
        private readonly ICalificacionRepository _califRepo;
        public RegistrarCalificacionHandler(ICalificacionRepository repo) { _califRepo = repo; }
        public async Task HandleAsync(CrearCalificacionCommand command) { }
    }
}
