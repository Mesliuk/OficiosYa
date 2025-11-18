using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class Trabajador
{
    public int Id { get; set; } // Es FK a Usuario.Id

    public string Documento { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public string? Bio { get; set; }

    public double RatingPromedio { get; set; }
    public int TotalTrabajosRealizados { get; set; }
    public bool Verificado { get; set; } = false;

    // Relación 1–1 con Usuario
    public Usuario Usuario { get; set; } = null!;

    // Relación N–N con Oficios
    public ICollection<TrabajadorOficio> Oficios { get; set; } = new List<TrabajadorOficio>();

    // Ubicación en tiempo real
    public UbicacionTrabajador? Ubicacion { get; set; }
}

