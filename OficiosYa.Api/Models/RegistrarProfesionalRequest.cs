using System.Collections.Generic;

namespace OficiosYa.Api.Models
{
    public class RegistrarProfesionalRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int OficioId { get; set; }
        public List<int> OficiosIds { get; set; } = new();
        public string? FotoPerfil { get; set; }

        // Dirección
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Alias { get; set; }
    }
}
