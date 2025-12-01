using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OficiosYa.Api.Models
{
    public class RegisterProfesionalRequest
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int OficioId { get; set; } = int.MaxValue;

        // Datos adicionales de un profesional
        [Required]
        public string Documento { get; set; } = string.Empty;

        // Now expect IDs provided by the Rubros/Oficios APIs
        public int RubroId { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
        
        public List<int>? OficiosIds { get; set; }
        // Removed FotoPerfil property: file is provided as IFormFile parameter in controller
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
    }
}

