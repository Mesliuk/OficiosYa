namespace OficiosYa.Domain.Entities;

public class Rubro
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Slug { get; set; }
    public string? Descripcion { get; set; }

    public ICollection<Oficio> Oficios { get; set; } = new List<Oficio>();
}
