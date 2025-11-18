using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class UbicacionTrabajador
{
    public int TrabajadorId { get; set; }
    public Trabajador Trabajador { get; set; } = null!;

    public double Latitud { get; set; }
    public double Longitud { get; set; }

    public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;
}

