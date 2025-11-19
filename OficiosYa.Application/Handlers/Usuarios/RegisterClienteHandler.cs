namespace OficiosYa.Application.Handlers.Usuarios
{
    using OficiosYa.Application.Commands.Usuarios;
    using OficiosYa.Application.DTOs;
    using OficiosYa.Application.Interfaces;
    using OficiosYa.Application.Utils;
    using OficiosYa.Domain.Entities;
    using OficiosYa.Domain.Enums;

    public class RegisterClienteHandler
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;


        public RegisterClienteHandler(IClienteRepository clienteRepo, IUsuarioRepository usuarioRepo)
        {
            _clienteRepository = clienteRepo;
            _usuarioRepository = usuarioRepo;
        }


        public async Task<UsuarioDto> HandleAsync(RegistrarClienteCommand command)
        {
            // Validar email
            var existing = await _usuarioRepository.ObtenerPorEmailAsync(command.Correo);
            if (existing != null) throw new Exception("El correo ya está registrado");


            // Crear usuario
            var usuario = new Usuario
            {
                Nombre = command.Nombre,
                Apellido = command.Apellido,
                Email = command.Correo,
                Telefono = command.Telefono,
                PasswordHash = PasswordHasher.Hash(command.Password),
                Rol = UsuarioRol.Cliente
            };


            await _usuarioRepository.AgregarAsync(usuario);


            // Crear cliente asociado
            var cliente = new Cliente { Usuario = usuario };
            await _clienteRepository.AgregarAsync(cliente);


            // Retornar DTO
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

