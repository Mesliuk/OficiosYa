using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class UbicacionProfesionalDto
    {
        public int ProfesionalId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
