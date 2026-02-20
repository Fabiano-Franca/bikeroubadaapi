using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    public class AlertaMapping : IEntityTypeConfiguration<Alerta>
    {
        public void Configure(EntityTypeBuilder<Alerta> builder)
        {
            builder.
                HasKey(e => e.Id);
            builder
                .HasOne(e => e.TipoAlerta)
                .WithMany(e => e.Alertas)
                .HasForeignKey(e => e.IdTipoAlerta)
                .IsRequired();

            builder.Property(e => e.Localizacao)
                .HasColumnType("point");

            builder.ToTable("Alertas");
        }
    }
}
