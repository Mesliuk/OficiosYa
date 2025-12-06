using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class CalificacionDto
    {
        [Required]
        public int EmisorId { get; set; }

        [Required]
        public int ReceptorId { get; set; }

        [Required]
        [Range(1,5)]
        public int Puntaje { get; set; }

        public string? Comentario { get; set; }
    }
}
