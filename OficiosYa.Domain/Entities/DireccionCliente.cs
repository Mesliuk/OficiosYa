namespace OficiosYa.Domain.Entities
{
    public class DireccionCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // Dirección del cliente (texto legible)
        public string Direccion { get; set; } = string.Empty;

        // Coordenadas
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        // Marca si es la dirección principal del cliente
        public bool EsPrincipal { get; set; }
    }
}