using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class UbicacionProfesional
{
    public int Id { get; set; }
    public int ProfesionalId { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;
    public Profesional Profesional { get; set; } = null!;
}

