namespace OficiosYa.Api.Models
{
    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // Optional role: "Cliente" or "Profesional". If not provided, will try any role.
        public string? Role { get; set; }
    }
}