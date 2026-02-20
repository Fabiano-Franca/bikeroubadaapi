using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRoubada.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adddestaque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Destaque",
                table: "Arquivos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destaque",
                table: "Arquivos");
        }
    }
}
