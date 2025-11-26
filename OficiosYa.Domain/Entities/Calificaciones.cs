using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int EmisorId { get; set; }
        public int ReceptorId { get; set; }
        public int UsuarioCalificaId { get; set; }
        public int UsuarioCalificadoId { get; set; }
        public int Puntaje { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;


        // Existing names
        public Usuario Emisor { get; set; } = null!;
        public Usuario Receptor { get; set; } = null!;

        // Added navigation properties matching configuration/migrations
        public Usuario UsuarioCalifica { get; set; } = null!;
        public Usuario UsuarioCalificado { get; set; } = null!;
    }
}
