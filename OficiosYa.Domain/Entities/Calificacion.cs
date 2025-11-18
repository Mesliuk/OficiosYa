using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class Calificacion
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudServicio Solicitud { get; set; } = null!;

    public int UsuarioCalificaId { get; set; }
    public Usuario UsuarioCalifica { get; set; } = null!;

    public int UsuarioCalificadoId { get; set; }
    public Usuario UsuarioCalificado { get; set; } = null!;

    public int Puntaje { get; set; }
    public string? Comentario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}

