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

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
