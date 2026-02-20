using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    public class RouboMapping : IEntityTypeConfiguration<Roubo>
    {
        public void Configure(EntityTypeBuilder<Roubo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NumeroBoletim)
                .IsRequired();

            builder.Property(e => e.DataRoubo) .IsRequired();

            builder.Property(e => e.Localizacao)
                .HasColumnType("point");
            builder
                .HasOne(e => e.Bicicleta)
                .WithMany(e => e.Roubos)
                .HasForeignKey(e => e.IdBicicleta)
                .IsRequired();

            builder.ToTable("Roubos");


        }
    }
}
