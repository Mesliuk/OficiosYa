using OficiosYa.Application.Commands.Oficios;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficio
{
    public class CreateOficioHandler
    {
        private readonly IOficioRepository _oficioRepo;
        public CreateOficioHandler(IOficioRepository repo) { _oficioRepo = repo; }
        public async Task HandleAsync(CrearOficioCommand command) { }
    }
}
