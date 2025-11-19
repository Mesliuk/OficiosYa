using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IPasswordResetService
    {
        Task<string> GenerarTokenAsync(int usuarioId);
        Task<bool> ValidarTokenAsync(string token);
        Task<bool> ResetearPasswordAsync(string token, string nuevoPassword);
    }
}
