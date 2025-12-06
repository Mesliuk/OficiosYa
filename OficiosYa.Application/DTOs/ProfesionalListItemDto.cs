namespace OficiosYa.Application.DTOs
{
    public class OficioSimpleDto
    {
        public int Id { get; set; }
        public int RubroId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? RubroNombre { get; set; }
    }

    public class ProfesionalListItemDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public double RatingPromedio { get; set; }
        public int TotalCalificaciones { get; set; }
        public string? FotoPerfil { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string? Direccion { get; set; }
        public List<OficioSimpleDto> Oficios { get; set; } = new List<OficioSimpleDto>();
    }
}
