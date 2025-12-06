namespace OficiosYa.Application.Handlers.Usuarios
{
    using OficiosYa.Application.DTOs;
    using OficiosYa.Application.Interfaces;
    using OficiosYa.Application.Utils;
    using OficiosYa.Domain.Entities;
    using OficiosYa.Domain.Enums;
    using System;

    public class RegisterClienteHandler
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public RegisterClienteHandler(IClienteRepository clienteRepo, IUsuarioRepository usuarioRepo)
        {
            _clienteRepository = clienteRepo;
            _usuarioRepository = usuarioRepo;
        }

        public async Task<UsuarioDto> HandleAsync(RegistroClienteDto dto)
        {
            // New DTO-based API
            var existing = await _usuarioRepository.ObtenerPorEmailAsync(dto.Email);
            if (existing != null) throw new Exception("El correo ya está registrado");

            string? fotoPath = dto.FotoPerfil;

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Rol = UsuarioRoleEnum.Cliente,
                FotoPerfil = null,
                Direccion = dto.Direccion ?? string.Empty,
                Latitud = dto.Latitud ?? 0,
                Longitud = dto.Longitud ?? 0
            };

            await _usuarioRepository.AgregarAsync(usuario);

            var cliente = new Cliente { Usuario = usuario, FotoPerfil = fotoPath };
            await _clienteRepository.AgregarAsync(cliente);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol.ToString(),
                FotoPerfil = cliente.FotoPerfil
            };
        }
    }
}

