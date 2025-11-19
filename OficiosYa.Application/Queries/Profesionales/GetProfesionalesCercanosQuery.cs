using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Queries.Profesionales
{
    public class GetProfesionalesCercanosQuery
    {
        public int OficioId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double RadioKm { get; set; }
    }
}
