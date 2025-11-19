using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Usuarios
{
    public class ResetPasswordHandler
    {
        private readonly IPasswordResetService _passwordResetService;
        public ResetPasswordHandler(IPasswordResetService service) { _passwordResetService = service; }


        public async Task<bool> HandleAsync(ResetPasswordCommand command)
        {
            return await _passwordResetService.ResetPasswordAsync(command.Codigo, command.NuevoPassword);
        }
    }
}
