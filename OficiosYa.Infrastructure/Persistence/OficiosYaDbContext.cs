using Microsoft.EntityFrameworkCore;
using OficiosYa.Domain.Entities; // Ajusta según el namespace de tus entidades

namespace OficiosYa.Infrastructure.Persistence
{
    public class OficiosYaDbContext : DbContext
    {
        public OficiosYaDbContext(DbContextOptions<OficiosYaDbContext> options)
            : base(options)
        { }

        // DbSets según tus entidades
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DireccionCliente> DireccionesClientes { get; set; }
        public DbSet<Oficio> Oficios { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<ProfesionalOficio> ProfesionalesOficios { get; set; }
        public DbSet<UbicacionProfesional> UbicacionesProfesionales { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones opcionales de Fluent API
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Profesional>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ProfesionalOficio>(entity =>
            {
                entity.HasKey(po => new { po.ProfesionalId, po.OficioId });

                entity.HasOne(po => po.Profesional)
                      .WithMany(p => p.ProfesionalesOficios)
                      .HasForeignKey(po => po.ProfesionalId);

                entity.HasOne(po => po.Oficio)
                      .WithMany(o => o.ProfesionalesOficios)
                      .HasForeignKey(po => po.OficioId);
            });

            // Agrega otras configuraciones necesarias para relaciones, longitudes y restricciones
        }
    }
}
