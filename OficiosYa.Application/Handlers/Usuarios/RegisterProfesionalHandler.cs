using OficiosYa.Application.Commands.Profesionales;
using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Utils;
using OficiosYa.Domain.Entities;
using OficiosYa.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Handlers.Usuarios
{
    public class RegisterProfesionalHandler
    {
        private readonly IProfesionalRepository _profRepo;
        private readonly IUsuarioRepository _usuarioRepo;


        public RegisterProfesionalHandler(IProfesionalRepository profRepo, IUsuarioRepository usuarioRepo)
        {
            _profRepo = profRepo;
            _usuarioRepo = usuarioRepo;
        }


        public async Task<UsuarioDto> HandleAsync(RegistrarProfesionalCommand command)
        {
            var existing = await _usuarioRepo.ObtenerPorEmailAsync(command.Correo);
            if (existing != null) throw new Exception("El correo ya está registrado");


            var usuario = new Usuario
            {
                Nombre = command.Nombre,
                Apellido = command.Apellido,
                Email = command.Correo,
                Telefono = command.Telefono,
                PasswordHash = PasswordHasher.Hash(command.Password),
                Rol = UsuarioRol.Profesional
            };


            await _usuarioRepo.AgregarAsync(usuario);


            var profesional = new OficiosYa.Domain.Entities.Profesional
            {
                Usuario = usuario,
                Documento = command.Documento,
                Bio = command.Bio,
                Verificado = false,
                RatingPromedio = 0,
                TotalCalificaciones = 0
            };


            await _profRepo.AgregarAsync(profesional);


            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString()
            };
        }
    }
}
