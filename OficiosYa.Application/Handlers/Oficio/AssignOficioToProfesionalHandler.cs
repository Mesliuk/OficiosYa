using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Oficio
{
    public class AssignOficioToProfesionalHandler
    {
        private readonly IProfesionalRepository _profRepo;
        private readonly IOficioRepository _oficioRepo;
        public AssignOficioToProfesionalHandler(IProfesionalRepository profRepo, IOficioRepository oficioRepo) { _profRepo = profRepo; _oficioRepo = oficioRepo; }
        public async Task HandleAsync(int profesionalId, int oficioId) { }
    }
}
