using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class SolicitudServicioConfiguration : IEntityTypeConfiguration<SolicitudServicio>
{
    public void Configure(EntityTypeBuilder<SolicitudServicio> builder)
    {
        builder.ToTable("SolicitudesServicio");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Estado)
            .HasConversion<int>();

        builder.Property(x => x.MetodoPago)
            .HasConversion<int>();

        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId);

        builder.HasOne(x => x.TrabajadorAsignado)
            .WithMany()
            .HasForeignKey(x => x.TrabajadorAsignadoId)
            .IsRequired(false);

        builder.HasMany(x => x.Candidatos)
            .WithOne(x => x.Solicitud)
            .HasForeignKey(x => x.SolicitudId);
    }
}

