using System.ComponentModel.DataAnnotations;

namespace OficiosYa.Api.Models
{
    public class RegisterClienteRequest
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
        public string Correo { get; set; } = string.Empty;   // o 'Correo' si prefieres

        [Required]
        [MaxLength(30)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        // Optional address/location fields that map to RegistrarClienteCommand
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Descripcion { get; set; }
    }
}

