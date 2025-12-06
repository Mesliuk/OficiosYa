using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Utils;
using OficiosYa.Domain.Enums;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Usuarios
{
    public class LoginUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public LoginUsuarioHandler(IUsuarioRepository usuarioRepo) { _usuarioRepo = usuarioRepo; }


        public async Task<UsuarioDto?> HandleAsync(LoginCommand command)
        {
            // If role specified, use ObtenerPorEmailYRolAsync
            var usuario = string.IsNullOrWhiteSpace(command.Role)
                ? await _usuarioRepo.ObtenerPorEmailAsync(command.Correo)
                : await _usuarioRepo.ObtenerPorEmailYRolAsync(command.Correo, Enum.Parse<UsuarioRoleEnum>(command.Role, true));

            if (usuario == null || !PasswordHasher.Verify(command.Password, usuario.PasswordHash))
                return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(),
                Token = null
            };
        }
    }
}
