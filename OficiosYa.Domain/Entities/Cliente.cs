using System.Collections.Generic;
using System.Linq;

namespace OficiosYa.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        // FK to Usuario
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;       
     
        // Direcciones del cliente
        public ICollection<DireccionCliente> Direcciones { get; set; } = new List<DireccionCliente>();

        // Acceso a la dirección principal (marcada con EsPrincipal)
        public DireccionCliente? DireccionPrincipal =>
            Direcciones.FirstOrDefault(d => d.EsPrincipal) ?? Direcciones.FirstOrDefault();

        // Propiedades delegadas para compatibilidad con código que espera campos simples
        public string Direccion => DireccionPrincipal?.Direccion ?? Usuario?.Direccion ?? string.Empty;
        public double Latitud => DireccionPrincipal?.Latitud ?? Usuario?.Latitud ?? 0;
        public double Longitud => DireccionPrincipal?.Longitud ?? Usuario?.Longitud ?? 0;

        // Foto de perfil almacenada por cliente (se puede setear desde controller)
        public string? FotoPerfil { get; set; }
    }
}
