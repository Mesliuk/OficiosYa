using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class UbicacionProfesionalDto
    {
        [Required]
        public int ProfesionalId { get; set; }

        [Required]
        [Range(-90,90)]
        public double Latitud { get; set; }

        [Required]
        [Range(-180,180)]
        public double Longitud { get; set; }

        // Friendly name/label for the location (e.g. "Casa", "Sucursal", "Calle Falsa 123")
        public string? NombreDireccion { get; set; }
    }
}
