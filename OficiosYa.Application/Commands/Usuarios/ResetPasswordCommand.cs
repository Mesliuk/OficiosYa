using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Usuarios
{
    public class ResetPasswordCommand
    {
        public string Correo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string NuevoPassword { get; set; } = string.Empty;

        public ResetPasswordCommand(string correo, string codigo, string nuevoPassword)
        {
            Correo = correo;
            Codigo = codigo;
            NuevoPassword = nuevoPassword;
        }
    }
}

