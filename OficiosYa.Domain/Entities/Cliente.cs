using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; } // FK Usuario
        public Usuario Usuario { get; set; } = null!;
        public ICollection<Calificacion>? CalificacionesRecibidas { get; set; }
        public ICollection<DireccionCliente>? Direcciones { get; set; }
    }
}
