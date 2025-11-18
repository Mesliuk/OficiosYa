using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class DireccionUsuario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string Alias { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;

    public double Latitud { get; set; }
    public double Longitud { get; set; }
}

