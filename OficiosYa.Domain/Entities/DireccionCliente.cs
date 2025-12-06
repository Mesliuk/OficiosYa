namespace OficiosYa.Domain.Entities
{
    public class DireccionCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public string Descripcion { get; set; } = "Ubicación";
        public string Direccion { get; set; } = string.Empty;
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        // Marca si esta es la dirección principal del cliente
        public bool EsPrincipal { get; set; }
    }
}
