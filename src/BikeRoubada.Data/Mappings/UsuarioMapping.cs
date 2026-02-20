using BikeRoubada.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRoubada.Data.Mappings
{
    internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(e => e.IdentificadorPessoal)
                .HasColumnType("varchar(14)")
                .IsRequired(false);

            builder.Property(e => e.Genero)
                .HasColumnType("varchar(20)");
            builder
                .HasMany(e => e.Enderecos)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .HasPrincipalKey(e => e.Id);
            builder
                .Property(e => e.Email)
                .HasColumnType("varchar(256)")
                .IsRequired();

            builder.Property(e => e.Telefone)
                .HasColumnType("varchar(15)");

        }
    }
}
