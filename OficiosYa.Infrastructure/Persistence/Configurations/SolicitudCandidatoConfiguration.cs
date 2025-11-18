using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Persistence.Configurations;

public class SolicitudCandidatoConfiguration : IEntityTypeConfiguration<SolicitudCandidato>
{
    public void Configure(EntityTypeBuilder<SolicitudCandidato> builder)
    {
        builder.ToTable("SolicitudesCandidatos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Estado)
            .HasConversion<int>();
    }
}

