using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class UbicacionProfesionalConfiguration : IEntityTypeConfiguration<UbicacionTrabajador>
{
    public void Configure(EntityTypeBuilder<UbicacionTrabajador> builder)
    {
        builder.ToTable("UbicacionesTrabajadores");

        builder.HasKey(x => x.TrabajadorId);
    }
}

