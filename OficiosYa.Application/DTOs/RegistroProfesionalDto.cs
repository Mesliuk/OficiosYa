using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class RegistroProfesionalDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Bio { get; set; } = "";
        public List<int> OficiosIds { get; set; } = new();
    }
}
