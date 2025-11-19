using OficiosYa.Application.Commands.Calificaciones;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Calificacion
{
    public class RegistrarCalificacionHandler
    {
        private readonly IClasificacionRepository _clasifRepo;
        public RegistrarCalificacionHandler(IClasificacionRepository repo) { _clasifRepo = repo; }
        public async Task HandleAsync(CrearCalificacionCommand command) { }
    }
}
