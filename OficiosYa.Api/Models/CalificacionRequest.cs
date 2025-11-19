namespace OficiosYa.Api.Models
{
    public class CalificacionRequest
    {
        public int EmisorId { get; set; }
        public int ReceptorId { get; set; }
        public int Puntaje { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
