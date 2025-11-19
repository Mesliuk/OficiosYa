using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Configurations;

public class DireccionClienteConfiguration : IEntityTypeConfiguration<DireccionCliente>
{
    public void Configure(EntityTypeBuilder<DireccionCliente> builder)
    {
        builder.ToTable("DireccionesClientes");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Direcciones)
            .HasForeignKey(x => x.ClienteId);
    }
}
