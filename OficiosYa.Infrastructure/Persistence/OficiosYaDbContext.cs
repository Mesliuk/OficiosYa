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
        public DbSet<Rubro> Rubros { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public DbSet<Profesional> Profesionales { get; set; } = null!;
        public DbSet<ProfesionalOficio> ProfesionalesOficios { get; set; } = null!;
        public DbSet<UbicacionProfesional> UbicacionesProfesionales { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Busca configuraciones de IEntityTypeConfiguration<T> en el ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OficiosYaDbContext).Assembly);

            // Seed Rubros
            modelBuilder.Entity<Rubro>().HasData(
                new Rubro { Id = 1, Nombre = "Construcción y mantenimiento", Slug = "construccion-y-mantenimiento", Descripcion = "Trabajos de construcción y mantenimiento" },
                new Rubro { Id = 2, Nombre = "Vehículos y mecánica", Slug = "vehiculos-y-mecanica", Descripcion = "Servicios para vehículos y mecánica" },
                new Rubro { Id = 3, Nombre = "Servicios para el hogar", Slug = "servicios-para-el-hogar", Descripcion = "Servicios domésticos y para el hogar" },
                new Rubro { Id = 4, Nombre = "Tecnología y digital", Slug = "tecnologia-y-digital", Descripcion = "Servicios técnicos y digitales" },
                new Rubro { Id = 5, Nombre = "Reparaciones varias", Slug = "reparaciones-varias", Descripcion = "Servicios de reparación diversos" },
                new Rubro { Id = 6, Nombre = "Gastronomía", Slug = "gastronomia", Descripcion = "Profesionales de cocina y gastronomía" },
                new Rubro { Id = 7, Nombre = "Artes y oficios manuales", Slug = "artes-y-oficios-manuales", Descripcion = "Oficios manuales y artísticos" }
            );

            // Seed Oficios
            modelBuilder.Entity<Oficio>().HasData(
                new Oficio { Id = 1, Nombre = "Albañil", RubroId = 1 },
                new Oficio { Id = 2, Nombre = "Plomero / Gasista (matriculado)", RubroId = 1 },
                new Oficio { Id = 3, Nombre = "Electricista", RubroId = 1 },
                new Oficio { Id = 4, Nombre = "Pintor", RubroId = 1 },
                new Oficio { Id = 5, Nombre = "Carpintero", RubroId = 1 },
                new Oficio { Id = 6, Nombre = "Herrero", RubroId = 1 },
                new Oficio { Id = 7, Nombre = "Vidriero", RubroId = 1 },
                new Oficio { Id = 8, Nombre = "Techista", RubroId = 1 },
                new Oficio { Id = 9, Nombre = "Yesero", RubroId = 1 },
                new Oficio { Id = 10, Nombre = "Colocador de durlock", RubroId = 1 },
                new Oficio { Id = 11, Nombre = "Colocador de cerámica / porcelanato", RubroId = 1 },
                new Oficio { Id = 12, Nombre = "Instalador de aire acondicionado", RubroId = 1 },
                new Oficio { Id = 13, Nombre = "Instalador de alarmas / cámaras de seguridad", RubroId = 1 },
                new Oficio { Id = 14, Nombre = "Jardinero", RubroId = 1 },
                new Oficio { Id = 15, Nombre = "Parquero", RubroId = 1 },
                new Oficio { Id = 16, Nombre = "Podador de árboles", RubroId = 1 },

                new Oficio { Id = 17, Nombre = "Mecánico automotor", RubroId = 2 },
                new Oficio { Id = 18, Nombre = "Chapista", RubroId = 2 },
                new Oficio { Id = 19, Nombre = "Pintor automotor", RubroId = 2 },
                new Oficio { Id = 20, Nombre = "Gomería", RubroId = 2 },
                new Oficio { Id = 21, Nombre = "Mecánico de motos", RubroId = 2 },
                new Oficio { Id = 22, Nombre = "Electricista automotor", RubroId = 2 },
                new Oficio { Id = 23, Nombre = "Lavadero de autos / Detailing", RubroId = 2 },

                new Oficio { Id = 24, Nombre = "Limpieza general", RubroId = 3 },
                new Oficio { Id = 25, Nombre = "Limpieza profunda", RubroId = 3 },
                new Oficio { Id = 26, Nombre = "Limpieza post-obra", RubroId = 3 },
                new Oficio { Id = 27, Nombre = "Niñera", RubroId = 3 },
                new Oficio { Id = 28, Nombre = "Cuidador de adultos mayores", RubroId = 3 },
                new Oficio { Id = 29, Nombre = "Mudanzas / Fletes", RubroId = 3 },
                new Oficio { Id = 30, Nombre = "Paseador de perros", RubroId = 3 },

                new Oficio { Id = 31, Nombre = "Técnico en PC / Notebook", RubroId = 4 },
                new Oficio { Id = 32, Nombre = "Técnico en celulares", RubroId = 4 },
                new Oficio { Id = 33, Nombre = "Instalador de redes", RubroId = 4 },
                new Oficio { Id = 34, Nombre = "Técnico en impresoras", RubroId = 4 },

                new Oficio { Id = 35, Nombre = "Cerrajero", RubroId = 5 },
                new Oficio { Id = 36, Nombre = "Tapicero", RubroId = 5 },
                new Oficio { Id = 37, Nombre = "Reparación de electrodomésticos", RubroId = 5 },
                new Oficio { Id = 38, Nombre = "Servicio técnico de heladeras", RubroId = 5 },
                new Oficio { Id = 39, Nombre = "Servicio técnico de lavarropas", RubroId = 5 },
                new Oficio { Id = 40, Nombre = "Técnico en TV", RubroId = 5 },

                new Oficio { Id = 41, Nombre = "Panadero", RubroId = 6 },
                new Oficio { Id = 42, Nombre = "Pastelero", RubroId = 6 },
                new Oficio { Id = 43, Nombre = "Cocinero", RubroId = 6 },
                new Oficio { Id = 44, Nombre = "Parrillero", RubroId = 6 },
                new Oficio { Id = 45, Nombre = "Bartender", RubroId = 6 },

                new Oficio { Id = 46, Nombre = "Costurera / Modista", RubroId = 7 },
                new Oficio { Id = 47, Nombre = "Sastre", RubroId = 7 },
                new Oficio { Id = 48, Nombre = "Artesano", RubroId = 7 },
                new Oficio { Id = 49, Nombre = "Zapatero", RubroId = 7 },
                new Oficio { Id = 50, Nombre = "Joyería / Relojería", RubroId = 7 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

