using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class Profesional
    {
        public int Id { get; set; } // FK Usuario
        public string Documento { get; set; } = null!;
        public string Bio { get; set; } = string.Empty;
        public bool Verificado { get; set; }
        public double RatingPromedio { get; set; }
        public int TotalCalificaciones { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public ICollection<ProfesionalOficio> ProfesionalesOficios { get; set; } = new List<ProfesionalOficio>();
        public ICollection<UbicacionProfesional> Ubicaciones { get; set; } = new List<UbicacionProfesional>();
        public ICollection<ProfesionalOficio> Oficios { get; set; } = new List<ProfesionalOficio>();
    }
}
