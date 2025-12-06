using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId).IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany() // Usuario doesn't have Clientes collection by default
                .HasForeignKey(x => x.UsuarioId);

            builder.Property(x => x.FotoPerfil)
                .HasColumnType("text")
                .IsRequired(false);
        }
    }
}
