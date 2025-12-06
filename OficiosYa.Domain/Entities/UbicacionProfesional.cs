namespace OficiosYa.Domain.Entities
{
    public class UbicacionProfesional
    {
        public int Id { get; set; }
        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; } = null!;

        public string NombreDireccion { get; set; } = "Ubicación";
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime? UltimaActualizacion { get; set; }
    }
}
