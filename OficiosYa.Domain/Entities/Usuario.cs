using OficiosYa.Domain.Enums;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public bool Activo { get; set; } = true;
    public string Direccion { get; set; } = null!;
    public double Latitud { get; set; }
    public double Longitud { get; set; }

    // Store profile photo as path/URL instead of binary
    public string? FotoPerfil { get; set; }
    public UsuarioRoleEnum Rol { get; set; }
    public ICollection<Calificacion> CalificacionesEmitidas { get; set; } = new List<Calificacion>();
    public ICollection<Calificacion> CalificacionesRecibidas { get; set; } = new List<Calificacion>();

    // Added to match EF configurations/migrations
    public ICollection<UsuarioRole> Roles { get; set; } = new List<UsuarioRole>();
}

