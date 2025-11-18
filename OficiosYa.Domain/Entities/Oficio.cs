using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class Oficio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? IconoUrl { get; set; }

    public ICollection<TrabajadorOficio> Trabajadores { get; set; } = new List<TrabajadorOficio>();
}

