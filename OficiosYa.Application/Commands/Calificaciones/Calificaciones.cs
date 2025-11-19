using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Calificaciones
{
    public class CrearCalificacionCommand
    {
        public int UsuarioCalificaId { get; set; }
        public int UsuarioCalificadoId { get; set; }
        public int Puntaje { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}

