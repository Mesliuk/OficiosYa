using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class ProfesionalConfiguration : IEntityTypeConfiguration<Trabajador>
{
    public void Configure(EntityTypeBuilder<Trabajador> builder)
    {
        builder.ToTable("Trabajadores");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Usuario)
            .WithOne()
            .HasForeignKey<Trabajador>(x => x.Id);

        builder.HasMany(x => x.Oficios)
            .WithOne(x => x.Trabajador)
            .HasForeignKey(x => x.TrabajadorId);

        builder.HasOne(x => x.Ubicacion)
            .WithOne(x => x.Trabajador)
            .HasForeignKey<UbicacionTrabajador>(x => x.TrabajadorId);
    }
}

