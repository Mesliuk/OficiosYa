using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

namespace OficiosYa.Infrastructure.Persistence.Configurations;

public class DireccionUsuarioConfiguration : IEntityTypeConfiguration<DireccionUsuario>
{
    public void Configure(EntityTypeBuilder<DireccionUsuario> builder)
    {
        builder.ToTable("DireccionesUsuario");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Direcciones)
            .HasForeignKey(x => x.UsuarioId);
    }
}

