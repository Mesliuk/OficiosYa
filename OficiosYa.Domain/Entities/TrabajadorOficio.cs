using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class TrabajadorOficio
{
    public int Id { get; set; }

    public int TrabajadorId { get; set; }
    public Trabajador Trabajador { get; set; } = null!;

    public int OficioId { get; set; }
    public Oficio Oficio { get; set; } = null!;

    public int AnosExperiencia { get; set; }
    public decimal PrecioHoraBase { get; set; }
    public string? CertificadosUrl { get; set; }
}
