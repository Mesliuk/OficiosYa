using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class ProfesionalConfiguration : IEntityTypeConfiguration<Profesional>
{
    public void Configure(EntityTypeBuilder<Profesional> builder)
    {
        builder.ToTable("Profesionales");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Usuario)
            .WithOne()
            .HasForeignKey<Profesional>(x => x.Id);

        builder.HasMany(x => x.Oficios)
            .WithOne(x => x.Profesional)
            .HasForeignKey(x => x.ProfesionalId);

        // Ubicacion property does not exist in Profesional entity
        // builder.HasOne(x => x.Ubicacion)
        //     .WithOne(x => x.Profesional)
        //     .HasForeignKey<UbicacionProfesional>(x => x.ProfesionalId);
    }
}

