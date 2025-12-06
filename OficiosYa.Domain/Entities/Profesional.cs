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
        public bool Verificado { get; set; }
        public double RatingPromedio { get; set; }
        public int TotalCalificaciones { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public ICollection<UbicacionProfesional> Ubicaciones { get; set; } = new List<UbicacionProfesional>();
        public ICollection<ProfesionalOficio> Oficios { get; set; } = new List<ProfesionalOficio>();  

        // Descripcion (replaces old 'Bio')
        public string? Descripcion { get; set; }

        // Foto perfil del profesional
        public string? FotoPerfil { get; set; }
    }
}


