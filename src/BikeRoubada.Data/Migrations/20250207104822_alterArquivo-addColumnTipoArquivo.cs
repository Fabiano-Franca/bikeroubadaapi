using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRoubada.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterArquivoaddColumnTipoArquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Enderecos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Enderecos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Arquivos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Arquivos");
        }
    }
}
