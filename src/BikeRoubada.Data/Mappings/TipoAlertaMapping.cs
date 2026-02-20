using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    public class TipoAlertaMapping : IEntityTypeConfiguration<TipoAlerta>
    {
        public void Configure(EntityTypeBuilder<TipoAlerta> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder
                .HasMany(e => e.Alertas)
                .WithOne(e => e.TipoAlerta)
                .HasForeignKey(e => e.IdTipoAlerta)
                .IsRequired();
        }
    }
}
