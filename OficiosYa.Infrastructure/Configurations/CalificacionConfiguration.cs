using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class CalificacionConfiguration : IEntityTypeConfiguration<Calificacion>
{
    public void Configure(EntityTypeBuilder<Calificacion> builder)
    {
        builder.ToTable("Calificaciones");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Emisor)
            .WithMany()
            .HasForeignKey(x => x.ReceptorId);

        builder.HasOne(x => x.Receptor)
            .WithMany()
            .HasForeignKey(x => x.ReceptorId);
    }
}
