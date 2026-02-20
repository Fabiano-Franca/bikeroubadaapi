using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRoubada.Data.Migrations
{
    /// <inheritdoc />
    public partial class altertables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Bicicletas_BicicletaId",
                table: "Arquivos");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_BicicletaId",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "BicicletaId",
                table: "Arquivos");

            migrationBuilder.AddColumn<string>(
                name: "Detalhes",
                table: "Bicicletas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdBicicleta",
                table: "Arquivos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_IdBicicleta",
                table: "Arquivos",
                column: "IdBicicleta");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos",
                column: "IdBicicleta",
                principalTable: "Bicicletas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_IdBicicleta",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "Detalhes",
                table: "Bicicletas");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdBicicleta",
                table: "Arquivos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BicicletaId",
                table: "Arquivos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_BicicletaId",
                table: "Arquivos",
                column: "BicicletaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Bicicletas_BicicletaId",
                table: "Arquivos",
                column: "BicicletaId",
                principalTable: "Bicicletas",
                principalColumn: "Id");
        }
    }
}
