using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    public class BicicletaMapping : IEntityTypeConfiguration<Bicicleta>
    {
        public void Configure(EntityTypeBuilder<Bicicleta> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Descricao)
                .HasColumnType("varchar(300)")
                .IsRequired();

            builder
                .HasOne(e => e.Endereco)
                .WithMany(e => e.Bicicletas)
                .HasForeignKey(e => e.IdEndereco)
                .IsRequired();

            builder.ToTable("Bicicletas");

            builder
                .HasOne(e => e.Usuario)
                .WithMany(e => e.Bicicletas)
                .HasForeignKey(e => e.IdUsuario)
                .IsRequired();

            builder
                .HasMany(e => e.Arquivos)
                .WithOne(e => e.Bicicleta)
                .HasForeignKey(e => e.IdBicicleta)
                .IsRequired();
        }
    }
}
