using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Rol { get; set; } = null!;
        public string? FotoPerfil { get; set; }

        // JWT token (nullable)
        public string? Token { get; set; }
    }
}
