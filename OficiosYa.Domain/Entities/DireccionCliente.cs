using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class DireccionCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Alias { get; set; } = "Ubicación";
        public string Direccion { get; set; } = null!;
        public double Latitud { get; set; }
        public double Longitud { get; set; }


        public Cliente Cliente { get; set; } = null!;
    }
}
