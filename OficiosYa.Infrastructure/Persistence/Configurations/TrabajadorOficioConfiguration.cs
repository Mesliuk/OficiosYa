using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Persistence.Configurations;

public class TrabajadorOficioConfiguration : IEntityTypeConfiguration<TrabajadorOficio>
{
    public void Configure(EntityTypeBuilder<TrabajadorOficio> builder)
    {
        builder.ToTable("TrabajadoresOficios");

        builder.HasKey(x => x.Id);
    }
}
