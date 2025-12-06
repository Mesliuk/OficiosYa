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

        builder.Property(x => x.Documento).IsRequired();

        builder.Property(x => x.FotoPerfil)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(x => x.Descripcion)
            .HasColumnType("text")
            .IsRequired(false);

        builder.HasOne(x => x.Usuario)
            .WithOne()
            .HasForeignKey<Profesional>(x => x.Id)
            .IsRequired();
    }
}

