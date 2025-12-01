using System.ComponentModel.DataAnnotations;

namespace OficiosYa.Application.DTOs
{
    public class OficioDto
    {
        public int Id { get; set; }

        [Required]
        public int RubroId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool RequiereLicencia { get; set; }
        public bool Activo { get; set; }
    }
}
