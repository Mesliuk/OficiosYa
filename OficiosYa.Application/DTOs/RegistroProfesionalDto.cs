using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class RegistroProfesionalDto : RegistroUsuarioBaseDto
    {
        public string Documento { get; set; } = null!;
        public System.Collections.Generic.List<int> OficiosIds { get; set; } = new();

        // Optional
        public string? FotoPerfil { get; set; }
        public string? Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Descripcion { get; set; }
    }
}
