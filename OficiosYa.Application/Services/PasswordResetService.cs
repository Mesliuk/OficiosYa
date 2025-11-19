using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class PasswordResetService
    {
        public Task<string> GenerarTokenAsync(int usuarioId) => Task.FromResult("");
        public Task<bool> ValidarTokenAsync(string token) => Task.FromResult(false);
        public Task<bool> ResetearPasswordAsync(string token, string nuevoPassword) => Task.FromResult(false);
    }
}
