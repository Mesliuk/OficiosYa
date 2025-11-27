using Microsoft.EntityFrameworkCore;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Persistence
{
    public class OficiosYaDbContext : DbContext
    {
        public OficiosYaDbContext(DbContextOptions<OficiosYaDbContext> options)
            : base(options)
        { }

        public DbSet<Calificacion> Calificaciones { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<DireccionCliente> DireccionesClientes { get; set; } = null!;
        public DbSet<Oficio> Oficios { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public DbSet<Profesional> Profesionales { get; set; } = null!;
        public DbSet<ProfesionalOficio> ProfesionalesOficios { get; set; } = null!;
        public DbSet<UbicacionProfesional> UbicacionesProfesionales { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Busca configuraciones de IEntityTypeConfiguration<T> en el ensamblado
            ModelBuilder modelBuilder1 = modelBuilder.ApplyConfigurationsFromAssembly(typeof(OficiosYaDbContext).Assembly);

            // Ejemplo de configuración por si no tenés archivo específico:
            // modelBuilder.Entity<ProfesionalOficio>()
            //     .HasKey(pf => new { pf.ProfesionalId, pf.OficioId });

            base.OnModelCreating(modelBuilder);
        }
    }
}

