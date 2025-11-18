using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities;

public class SolicitudServicio
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Usuario Cliente { get; set; } = null!;

    public int OficioId { get; set; }
    public Oficio Oficio { get; set; } = null!;

    public string DescripcionProblema { get; set; } = string.Empty;

    public int DireccionId { get; set; }
    public DireccionUsuario Direccion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public EstadoSolicitudServicio Estado { get; set; } = EstadoSolicitudServicio.Pendiente;

    public decimal PrecioEstimado { get; set; }
    public decimal? PrecioFinal { get; set; }

    public int? TrabajadorAsignadoId { get; set; }
    public Trabajador? TrabajadorAsignado { get; set; }

    public MetodoPago MetodoPago { get; set; }

    public ICollection<SolicitudCandidato> Candidatos { get; set; } = new List<SolicitudCandidato>();
}

public enum EstadoSolicitudServicio
{
    Pendiente = 1,
    BuscandoTrabajador = 2,
    Aceptado = 3,
    EnCurso = 4,
    Finalizado = 5,
    Cancelado = 6
}

public enum MetodoPago
{
    Efectivo = 1,
    MercadoPago = 2,
    Tarjeta = 3
}

