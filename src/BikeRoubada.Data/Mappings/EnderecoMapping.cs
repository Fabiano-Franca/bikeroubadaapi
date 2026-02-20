using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    internal class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Rua)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Estado)
                .HasColumnType("varchar(50)")
                .IsRequired();
            
            builder.Property(e => e.Cep)
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder
                .HasOne(e => e.Usuario)
                .WithMany(e => e.Enderecos)
                .HasForeignKey(e => e.IdUsuario);

            builder
                .HasMany(e => e.Bicicletas)
                .WithOne(e => e.Endereco)
                .HasForeignKey(e => e.IdEndereco)
                .IsRequired();

            builder.ToTable("Enderecos");
        }
    }
}
