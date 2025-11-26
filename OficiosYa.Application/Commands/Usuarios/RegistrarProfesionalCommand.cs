using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Usuarios
{
    public class RegistrarProfesionalCommand
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int OficioId { get; set; }

        public string? FotoPerfil { get; set; }
        public RegistrarProfesionalCommand(
            string nombre,
            string apellido,
            string correo,
            string telefono,
            string password,
            string documento,
            string bio,
            int oficioId,
            string? fotoPerfil = null)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Password = password;
            Documento = documento;
            Bio = bio;
            OficioId = oficioId;
            FotoPerfil = fotoPerfil;
        }
    }
}

