using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class ProfesionalOficio
    {
        public int Id { get; set; }
        public int ProfesionalId { get; set; }
        public int OficioId { get; set; }
        public int AnosExperiencia { get; set; }
        public Profesional Profesional { get; set; } = null!;
        public Oficio Oficio { get; set; } = null!;


    }
}
