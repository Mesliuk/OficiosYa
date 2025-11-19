using OficiosYa.Domain.Enums;

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
    public UsuarioRol Rol { get; set; }
}

