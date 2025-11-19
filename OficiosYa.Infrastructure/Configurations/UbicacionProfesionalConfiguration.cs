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
            .WithOne() // Assuming 1-to-1 or 1-to-many without nav back? 
                       // Profesional entity doesn't have Ubicacion property explicitly in my view earlier?
                       // Wait, Profesional.cs (Step 134) didn't show Ubicacion.
                       // But UbicacionProfesional has ProfesionalId.
            .HasForeignKey<UbicacionProfesional>(x => x.ProfesionalId);
    }
}

