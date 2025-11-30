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

        // Map Emisor/Receptor navigations to the EmisorId/ReceptorId foreign keys
        builder.HasOne(x => x.Emisor)
            .WithMany(u => u.CalificacionesEmitidas)
            .HasForeignKey(x => x.EmisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receptor)
            .WithMany(u => u.CalificacionesRecibidas)
            .HasForeignKey(x => x.ReceptorId)
            .OnDelete(DeleteBehavior.Restrict);

        // If there are duplicate properties like UsuarioCalifica/UsuarioCalificado in the entity,
        // they should be removed from the entity to avoid ambiguity. The configuration above
        // matches the current DTOs and repositories that use EmisorId/ReceptorId.
    }
}
