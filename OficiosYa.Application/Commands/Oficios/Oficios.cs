using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Oficios
{
    public class CrearOficioCommand
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class ActualizarOficioCommand
    {
        public int Id { get; set; }
        public string NuevoNombre { get; set; } = string.Empty;
    }
}
