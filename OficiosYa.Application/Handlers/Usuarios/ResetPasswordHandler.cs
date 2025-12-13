using System.Threading.Tasks;
using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Utils;

namespace OficiosYa.Application.Handlers.Usuarios
{
    public class ResetPasswordHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ResetPasswordHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> HandleAsync(ResetPasswordCommand command)
        {
            if (command == null) return false;

            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(command.Correo?.Trim() ?? string.Empty);
            if (usuario == null) return false;

            var hashed = PasswordHasher.Hash(command.NuevoPassword ?? string.Empty);

            var updated = await _usuarioRepository.UpdatePasswordAsync(usuario.Id, hashed);
            return updated;
        }
    }
}
