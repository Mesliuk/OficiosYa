using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class Oficio
{
    public int Id { get; set; }
    public int RubroId { get; set; }
    public Rubro Rubro { get; set; } = null!;

    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = string.Empty;
    public bool RequiereLicencia { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<ProfesionalOficio> Profesionales { get; set; } = new List<ProfesionalOficio>();
}
