using OficiosYa.Application.Commands.Profesionales;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Profesional
{
    public class UpdateProfesionalHandler
    {
        private readonly IProfesionalRepository _profRepo;
        public UpdateProfesionalHandler(IProfesionalRepository repo) { _profRepo = repo; }
        public async Task HandleAsync(Profesional prof) { }
    }
}
