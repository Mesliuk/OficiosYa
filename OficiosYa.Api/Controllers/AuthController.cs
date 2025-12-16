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
using System.Net.Mail;
using System.Security.Cryptography;
using System.Collections.Concurrent;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUsuarioHandler _loginUsuarioHandler;
        private readonly ResetPasswordHandler _resetPasswordHandler;
        private readonly IConfiguration _config;

        // Almacén en memoria para códigos de reseteo (simple, thread-safe)
        private static readonly PasswordResetStore _tokenStore = new PasswordResetStore();

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
        // SOLICITAR ENVÍO DE CÓDIGO POR EMAIL PARA RESET
        // =====================
        public class RequestPasswordRequest
        {
            public string Correo { get; set; } = string.Empty;
        }

        [HttpPost("password/request")]
        public async Task<IActionResult> RequestPassword(RequestPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Correo))
                return BadRequest("Correo es requerido");

            var email = request.Correo.Trim();

            // Generar código y guardarlo en el almacén
            var code = _tokenStore.GenerateAndStore(email, TimeSpan.FromMinutes(15));

            // Construir link de reset (frontend debe tener ruta que capture email+code y permita nueva contraseña)
            var frontendUrl = _config.GetValue<string>("Frontend:Url") ?? "https://localhost:3000";
            var resetPath = _config.GetValue<string>("Frontend:ResetPath") ?? "/reset-password";
            var resetUrl = $"{frontendUrl.TrimEnd('/')}{resetPath}?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(code)}";

            // Leer configuración SMTP
            var smtpSection = _config.GetSection("Smtp");
            var smtpHost = smtpSection.GetValue<string>("Host") ?? "sandbox.smtp.mailtrap.io";
            var smtpPort = smtpSection.GetValue<int?>("Port") ?? 2525;
            var smtpUser = smtpSection.GetValue<string>("Username") ?? "d4f7ab7d2dda5a";
            var smtpPass = smtpSection.GetValue<string>("Password") ?? "11991b46ea376e";
            var smtpFrom = smtpSection.GetValue<string>("From") ?? "d4f7ab7d2dda5a@mailtrap.io";
            var smtpEnableSsl = smtpSection.GetValue<bool?>("EnableSsl") ?? false;

            var subject = "Restablecer contraseña - OficiosYa";
            var body = $@"
                <p>Hola,</p>
                <p>Has solicitado restablecer tu contraseña. Haz clic en el siguiente enlace para continuar:</p>
                <p><a href=""{resetUrl}"">Restablecer contraseña</a></p>
                <p>Si no puedes hacer clic en el enlace, copia y pega esta URL en tu navegador:</p>
                <p>{resetUrl}</p>
                <p>Este enlace expirará en 15 minutos.</p>
                <p>Si no solicitaste este cambio, ignora este correo.</p>
            ";

            try
            {
                using var message = new MailMessage(smtpFrom, email)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    EnableSsl = smtpEnableSsl
                };

                if (!string.IsNullOrEmpty(smtpUser))
                {
                    client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
                }

                // Enviar email asíncronamente

                await client.SendMailAsync(message);

                return Ok(new { Message = "Email enviado si el correo existe en el sistema" });
            }
            catch (Exception ex)
            {
                // No exponer detalles de excepción en producción
                return StatusCode(500, "Error al enviar el correo de recuperación");
            }
        }

        // =====================
        // RECUPERAR CONTRASEÑA (VALIDA CÓDIGO LOCALMENTE ANTES DE LLAMAR AL HANDLER)
        // =====================
        [HttpPost("password/recover")]
        public async Task<IActionResult> Recuperar(MyResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Correo) || string.IsNullOrWhiteSpace(request?.Codigo) || string.IsNullOrWhiteSpace(request?.NuevaPassword))
                return BadRequest("Faltan datos requeridos");

            // Validar código contra el almacén en memoria
            var valid = _tokenStore.ValidateAndConsume(request.Correo.Trim(), request.Codigo.Trim());
            if (!valid)
                return BadRequest("Código inválido o expirado");

            var command = new ResetPasswordCommand(
                request.Correo,
                request.Codigo,
                request.NuevaPassword
            );

            var result = await _resetPasswordHandler.HandleAsync(command);
            return Ok(result);
        }

        // =====================
        // IMPLEMENTACIÓN SIMPLE DE ALMACÉN DE CÓDIGOS EN MEMORIA
        // =====================
        private class PasswordResetStore
        {
            private record TokenInfo(string Code, DateTimeOffset Expiry);

            private readonly ConcurrentDictionary<string, TokenInfo> _store = new();

            // Genera código seguro de 6 dígitos y lo almacena asociándolo al email
            public string GenerateAndStore(string email, TimeSpan ttl)
            {
                var code = GenerateCode6Digits();
                var expiry = DateTimeOffset.UtcNow.Add(ttl);
                _store.AddOrUpdate(email.ToLowerInvariant(), new TokenInfo(code, expiry), (k, v) => new TokenInfo(code, expiry));
                return code;
            }

            // Valida el código y lo consume (lo elimina si es válido)
            public bool ValidateAndConsume(string email, string code)
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
                    return false;

                var key = email.ToLowerInvariant();
                if (_store.TryGetValue(key, out var info))
                {
                    if (info.Code == code && info.Expiry >= DateTimeOffset.UtcNow)
                    {
                        _store.TryRemove(key, out _);
                        return true;
                    }

                    // Si expiró, eliminar
                    if (info.Expiry < DateTimeOffset.UtcNow)
                    {
                        _store.TryRemove(key, out _);
                    }
                }

                return false;
            }

            private static string GenerateCode6Digits()
            {
                Span<byte> bytes = stackalloc byte[4]; // 32 bits
                RandomNumberGenerator.Fill(bytes);
                // Convert to uint and mod 1_000_000
                var num = BitConverter.ToUInt32(bytes) % 1_000_000;
                return num.ToString("D6");
            }
        }
    }
}

