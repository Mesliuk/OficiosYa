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

        builder.Property(x => x.Nombre).HasMaxLength(100);
    }
}

