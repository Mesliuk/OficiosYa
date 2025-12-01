using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OficiosYa.Application.DTOs
{
    public class RubroDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = null!;
        public string? Slug { get; set; }
        public string? Descripcion { get; set; }
        public IList<OficioDto> Oficios { get; set; } = new List<OficioDto>();
    }
}
