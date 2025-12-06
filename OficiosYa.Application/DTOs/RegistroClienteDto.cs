using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class RegistroClienteDto : RegistroUsuarioBaseDto
    {
        // Address/location fields optional
        //public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Descripcion { get; set; }

        public string? FotoPerfil { get; set; }
    }
}
