using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class RubroConfiguration : IEntityTypeConfiguration<Rubro>
{
    public void Configure(EntityTypeBuilder<Rubro> builder)
    {
        builder.ToTable("Rubros");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Slug).HasMaxLength(150);
    }
}
