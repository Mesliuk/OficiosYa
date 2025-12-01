using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class OficioConfiguration : IEntityTypeConfiguration<Oficio>
{
    public void Configure(EntityTypeBuilder<Oficio> builder)
    {
        builder.ToTable("Oficios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Descripcion).IsRequired(false);
        builder.Property(x => x.RequiereLicencia).HasDefaultValue(false);
        builder.Property(x => x.Activo).HasDefaultValue(true);

        builder.HasOne(x => x.Rubro)
            .WithMany(r => r.Oficios)
            .HasForeignKey(x => x.RubroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

