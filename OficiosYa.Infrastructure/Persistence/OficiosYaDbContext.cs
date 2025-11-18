using Microsoft.EntityFrameworkCore;
using OficiosYa.Domain.Entities;
using OficiosYa.Domain.Enums;

namespace OficiosYa.Infrastructure.Persistence;

public class OficiosYaDbContext : DbContext
{
    public OficiosYaDbContext(DbContextOptions<OficiosYaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UsuarioRol> UsuariosRoles => Set<UsuarioRol>();
    public DbSet<Trabajador> Trabajadores => Set<Trabajador>();
    public DbSet<Oficio> Oficios => Set<Oficio>();
    public DbSet<TrabajadorOficio> TrabajadoresOficios => Set<TrabajadorOficio>();
    public DbSet<DireccionUsuario> DireccionesUsuarios => Set<DireccionUsuario>();
    public DbSet<UbicacionTrabajador> UbicacionesTrabajadores => Set<UbicacionTrabajador>();
    public DbSet<SolicitudServicio> SolicitudesServicios => Set<SolicitudServicio>();
    public DbSet<SolicitudCandidato> SolicitudesCandidatos => Set<SolicitudCandidato>();
    public DbSet<Calificacion> Calificaciones => Set<Calificacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OficiosYaDbContext).Assembly);
    }
}
