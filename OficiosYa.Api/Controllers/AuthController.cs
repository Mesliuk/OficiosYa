using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OficiosYa.Api.Models;
using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.Handlers.Usuarios;
using MyLoginRequest = OficiosYa.Api.Models.LoginRequest;
using MyResetPasswordRequest = OficiosYa.Api.Models.ResetPasswordRequest;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterClienteHandler _registerClienteHandler;
        private readonly RegisterProfesionalHandler _registerProfesionalHandler;
        private readonly LoginUsuarioHandler _loginUsuarioHandler;
        private readonly ResetPasswordHandler _resetPasswordHandler;

        public AuthController(
            RegisterClienteHandler registerClienteHandler,
            RegisterProfesionalHandler registerProfesionalHandler,
            LoginUsuarioHandler loginUsuarioHandler,
            ResetPasswordHandler resetPasswordHandler)
        {
            _registerClienteHandler = registerClienteHandler;
            _registerProfesionalHandler = registerProfesionalHandler;
            _loginUsuarioHandler = loginUsuarioHandler;
            _resetPasswordHandler = resetPasswordHandler;
        }

        // =====================
        // REGISTRO CLIENTE
        // =====================
        [HttpPost("registrar/cliente")]
        public async Task<IActionResult> RegistrarCliente(RegisterClienteRequest request)
        {
            var command = new RegistrarClienteCommand(
                request.Nombre,
                request.Apellido,
                request.Correo,
                request.Telefono,
                request.Password,
                request.FotoPerfil
            );

            var result = await _registerClienteHandler.HandleAsync(command);
            return Ok(result);
        }

        // =====================
        // REGISTRO PROFESIONAL
        // =====================
        [HttpPost("registrar/profesional")]
        public async Task<IActionResult> RegistrarProfesional(RegisterProfesionalRequest request)
        {
            var command = new RegistrarProfesionalCommand(
                request.Nombre,
                request.Apellido,
                request.Correo,
                request.Telefono,
                request.Password,
                request.Documento,
                request.Descripcion, // Bio
                request.OficioId,
                request.FotoPerfil
            );

            var result = await _registerProfesionalHandler.HandleAsync(command);
            return Ok(result);
        }

        // =====================
        // LOGIN
        // =====================
        [HttpPost("login")]
        public async Task<IActionResult> Login(MyLoginRequest request)
        {
            var command = new LoginCommand(request.Correo, request.Password);
            var result = await _loginUsuarioHandler.HandleAsync(command);

            if (result == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(result);
        }

        // =====================
        // RECUPERAR CONTRASEÑA
        // =====================
        [HttpPost("password/recover")]
        public async Task<IActionResult> Recuperar(MyResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(
                request.Correo,
                request.Codigo,
                request.NuevaPassword
                );
            var result = await _resetPasswordHandler.HandleAsync(command);
            return Ok(result);
        }
    }
}

