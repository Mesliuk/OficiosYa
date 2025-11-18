using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class UsuarioRol
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public RolUsuario Rol { get; set; }
}

public enum RolUsuario
{
    Cliente = 1,
    Trabajador = 2,
    Admin = 3
}

