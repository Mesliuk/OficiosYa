using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Usuarios
{
    public class RegistrarClienteCommand
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string? FotoPerfil { get; set; }
        public RegistrarClienteCommand(string nombre, string apellido, string correo, string telefono, string password, string? fotoPerfil = null)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Password = password;
            FotoPerfil = fotoPerfil;
        }
    }
}
