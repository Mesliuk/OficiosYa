namespace OficiosYa.Api.Controllers
{
    public class UpdateProfesionalRequest
    {
        // Usuario fields
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? UsuarioEmail { get; set; }

        // Profesional fields
        public string Documento { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public System.Collections.Generic.List<int>? OficiosIds { get; set; }
        public bool? Verificado { get; set; }
        public double? RatingPromedio { get; set; }
        public int? TotalCalificaciones { get; set; }
    }
}