using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public string? FotoUrl { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public bool Activo { get; set; } = true;

    // Relación N-N con roles
    public ICollection<UsuarioRol> Roles { get; set; } = new List<UsuarioRol>();

    // Relación 1-N con direcciones
    public ICollection<DireccionUsuario> Direcciones { get; set; } = new List<DireccionUsuario>();
}

