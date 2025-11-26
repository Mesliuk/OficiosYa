using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficiosYa.Domain.Entities;

public class ProfesionalOficioConfiguration : IEntityTypeConfiguration<ProfesionalOficio>
{
    public void Configure(EntityTypeBuilder<ProfesionalOficio> builder)
    {
        builder.HasKey(po => po.Id);

        builder.HasOne(po => po.Profesional)
               .WithMany(p => p.ProfesionalesOficios)
               .HasForeignKey(po => po.ProfesionalId);

        builder.HasOne(po => po.Oficio)
               .WithMany(o => o.Profesionales)
               .HasForeignKey(po => po.OficioId);
    }
}

