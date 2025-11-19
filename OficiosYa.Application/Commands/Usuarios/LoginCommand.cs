using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Commands.Usuarios
{
    public class LoginCommand
    {
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginCommand(string correo, string password)
        {  Correo = correo; 
           Password = password;}
    }
}
