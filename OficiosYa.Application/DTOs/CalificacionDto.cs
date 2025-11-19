using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class CalificacionDto
    {
        public int EmisorId { get; set; }
        public int ReceptorId { get; set; }
        public int Puntaje { get; set; }
        public string? Comentario { get; set; }
    }
}
