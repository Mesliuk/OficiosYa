namespace OficiosYa.Api.Models
{
    public class RegisterClienteRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;   // o 'Correo' si prefieres
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

