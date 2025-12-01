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
        private readonly IOficioRepository _oficioRepo;
        private readonly IUbicacionRepository _ubicacionRepo;

        public RegisterProfesionalHandler(IProfesionalRepository profRepo, IUsuarioRepository usuarioRepo, IOficioRepository oficioRepo, IUbicacionRepository ubicacionRepo)
        {
            _profRepo = profRepo;
            _usuarioRepo = usuarioRepo;
            _oficioRepo = oficioRepo;
            _ubicacionRepo = ubicacionRepo;
        }

        public async Task<UsuarioDto> HandleAsync(RegistroProfesionalDto dto)
        {
            var existingProfesional = await _usuarioRepo.ObtenerPorEmailYRolAsync(dto.Email, UsuarioRoleEnum.Profesional);
            if (existingProfesional != null) throw new Exception("El correo ya está registrado para un profesional");

            string? fotoPath = dto.FotoPerfil;

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Rol = UsuarioRoleEnum.Profesional,
                FotoPerfil = null
            };

            await _usuarioRepo.AgregarAsync(usuario);

            var profesional = new OficiosYa.Domain.Entities.Profesional
            {
                Usuario = usuario,
                Documento = dto.Documento,
                Verificado = false,
                RatingPromedio = 0,
                TotalCalificaciones = 0,
                FotoPerfil = fotoPath,
                Descripcion = dto.Descripcion
            };

            if (dto.OficiosIds != null && dto.OficiosIds.Any())
            {
                foreach (var oficioId in dto.OficiosIds)
                {
                    var oficio = await _oficioRepo.ObtenerPorIdAsync(oficioId);
                    if (oficio != null)
                    {
                        profesional.Oficios.Add(new ProfesionalOficio { Oficio = oficio, OficioId = oficioId });
                    }
                }
            }

            await _profRepo.AgregarAsync(profesional);

            if (dto.Latitud.HasValue && dto.Longitud.HasValue)
            {
                var ubDto = new UbicacionProfesionalDto
                {
                    ProfesionalId = profesional.Id,
                    Latitud = dto.Latitud.Value,
                    Longitud = dto.Longitud.Value
                };
                await _ubicacionRepo.RegistrarUbicacionAsync(ubDto);
            }

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(),
                FotoPerfil = profesional.FotoPerfil
            };
        }
    }
}
