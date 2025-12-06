using OficiosYa.Domain.Enums;

namespace OficiosYa.Domain.Entities
{
    public class UsuarioRole
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioRoleEnum Rol { get; set; }

        public Usuario Usuario { get; set; } = null!;
    }
}
