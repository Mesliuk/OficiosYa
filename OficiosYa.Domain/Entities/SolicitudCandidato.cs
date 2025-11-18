using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class SolicitudCandidato
{
    public int Id { get; set; }

    public int SolicitudId { get; set; }
    public SolicitudServicio Solicitud { get; set; } = null!;

    public int TrabajadorId { get; set; }
    public Trabajador Trabajador { get; set; } = null!;

    public EstadoCandidato Estado { get; set; }
    public double DistanciaCliente { get; set; }
    public int TiempoEstimadoLlegada { get; set; }
}

public enum EstadoCandidato
{
    Notificado = 1,
    Rechazado = 2,
    Aceptado = 3
}
