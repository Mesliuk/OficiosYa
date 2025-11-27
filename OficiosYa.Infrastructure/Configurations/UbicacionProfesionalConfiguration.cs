using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class UbicacionProfesionalConfiguration : IEntityTypeConfiguration<UbicacionProfesional>
{
    public void Configure(EntityTypeBuilder<UbicacionProfesional> builder)
    {
        builder.ToTable("UbicacionesProfesionales");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Profesional)
            .WithMany(p => p.Ubicaciones) // <-- corregido: 1 profesional puede tener muchas ubicaciones
            .HasForeignKey(x => x.ProfesionalId)
            .IsRequired();
    }
}

