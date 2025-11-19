using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Usuarios
{
    public class LoginUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepo;


        public LoginUsuarioHandler(IUsuarioRepository usuarioRepo) { _usuarioRepo = usuarioRepo; }


        public async Task<UsuarioDto?> HandleAsync(LoginCommand command)
        {
            var usuario = await _usuarioRepo.ObtenerPorEmailAsync(command.Correo);
            if (usuario == null || !PasswordHasher.Verify(command.Password, usuario.PasswordHash))
                return null;


            // Generar token (ejemplo simple)
            string token = TokenGenerator.GenerateToken(usuario.Email);


            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(),
                // Token puede agregarse al DTO si querés
            };
        }
    }
}
