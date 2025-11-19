namespace OficiosYa.Api.Models
{
    public class ResetPasswordRequest
    {
        public string Correo { get; set; } = string.Empty;

        public string Codigo {  get; set; } = string.Empty;
        public string NuevaPassword { get; set; } = string.Empty;
    }
}