using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OficiosYa.Api.Models;
using OficiosYa.Application.Commands.Usuarios;
using OficiosYa.Application.Handlers.Usuarios;
using MyLoginRequest = OficiosYa.Api.Models.LoginRequest;
using MyResetPasswordRequest = OficiosYa.Api.Models.ResetPasswordRequest;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUsuarioHandler _loginUsuarioHandler;
        private readonly ResetPasswordHandler _resetPasswordHandler;
        private readonly IConfiguration _config;

        public AuthController(
            LoginUsuarioHandler loginUsuarioHandler,
            ResetPasswordHandler resetPasswordHandler,
            IConfiguration config)
        {
            _loginUsuarioHandler = loginUsuarioHandler;
            _resetPasswordHandler = resetPasswordHandler;
            _config = config;
        }

        // =====================
        // LOGIN
        // =====================
        [HttpPost("login")]
        public async Task<IActionResult> Login(MyLoginRequest request)
        {
            var command = new LoginCommand(request.Correo, request.Password, request.Role);
            var result = await _loginUsuarioHandler.HandleAsync(command);

            if (result == null)
                return Unauthorized("Credenciales inválidas");

            // generar JWT y adjuntarlo al DTO
            var jwt = _config.GetSection("Jwt");
            var key = jwt.GetValue<string>("Key") ?? "default_dev_key_change_this";
            var issuer = jwt.GetValue<string>("Issuer") ?? "OficiosYa";
            var audience = jwt.GetValue<string>("Audience") ?? "OficiosYaAudience";
            var expiresMinutes = jwt.GetValue<int?>("ExpiresMinutes") ?? 60;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Role, result.Rol)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            result.Token = new JwtSecurityTokenHandler().WriteToken(token);

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

