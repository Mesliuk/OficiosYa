using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Usuarios
{
    public class SolicitarResetPasswordCommand
    {
        public string Correo { get; set; } = string.Empty;

        public SolicitarResetPasswordCommand(string correo)
        {
            Correo = correo;
        }
    }
}
