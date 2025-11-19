using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class ProfesionalOficioConfiguration : IEntityTypeConfiguration<ProfesionalOficio>
{
    public void Configure(EntityTypeBuilder<ProfesionalOficio> builder)
    {
        builder.ToTable("ProfesionalesOficios");

        builder.HasKey(x => x.Id);
    }
}
