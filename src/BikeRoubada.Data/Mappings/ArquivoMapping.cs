using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    public class ArquivoMapping : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NomeArquivo)
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder
                .HasOne(e => e.Roubo)
                .WithMany(e => e.Arquivos)
                .HasForeignKey(e => e.IdRoubo)
                .IsRequired(false);

            builder.ToTable("Arquivos");
        }
    }
}
