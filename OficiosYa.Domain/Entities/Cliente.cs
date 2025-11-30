namespace OficiosYa.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        // FK to Usuario
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;

        // Direcciones del cliente
        public ICollection<DireccionCliente> Direcciones { get; set; } = new List<DireccionCliente>();
    }
}
