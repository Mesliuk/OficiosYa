namespace OficiosYa.Api.Models
{
    public class RegisterProfesionalRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int OficioId { get; set; } = int.MaxValue;

        // Datos adicionales de un profesional
        public string Documento { get; set; } = string.Empty;
        public string Rubro { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;

        
    }
}

