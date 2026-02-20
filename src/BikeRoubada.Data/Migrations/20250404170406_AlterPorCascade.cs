using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRoubada.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterPorCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_Bicicletas_BicicletaId",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_TiposAlertas_IdTipoAlerta",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_Usuarios_UsuarioGeradorId",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Roubos_IdRoubo",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Bicicletas_Enderecos_IdEndereco",
                table: "Bicicletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Bicicletas_Usuarios_IdUsuario",
                table: "Bicicletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_IdUsuario",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Roubos_Bicicletas_IdBicicleta",
                table: "Roubos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_Bicicletas_BicicletaId",
                table: "Alertas",
                column: "BicicletaId",
                principalTable: "Bicicletas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_TiposAlertas_IdTipoAlerta",
                table: "Alertas",
                column: "IdTipoAlerta",
                principalTable: "TiposAlertas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_Usuarios_UsuarioGeradorId",
                table: "Alertas",
                column: "UsuarioGeradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos",
                column: "IdBicicleta",
                principalTable: "Bicicletas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Roubos_IdRoubo",
                table: "Arquivos",
                column: "IdRoubo",
                principalTable: "Roubos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Enderecos_IdEndereco",
                table: "Bicicletas",
                column: "IdEndereco",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Usuarios_IdUsuario",
                table: "Bicicletas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_IdUsuario",
                table: "Enderecos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roubos_Bicicletas_IdBicicleta",
                table: "Roubos",
                column: "IdBicicleta",
                principalTable: "Bicicletas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_Bicicletas_BicicletaId",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_TiposAlertas_IdTipoAlerta",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Alertas_Usuarios_UsuarioGeradorId",
                table: "Alertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Roubos_IdRoubo",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Bicicletas_Enderecos_IdEndereco",
                table: "Bicicletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Bicicletas_Usuarios_IdUsuario",
                table: "Bicicletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_IdUsuario",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Roubos_Bicicletas_IdBicicleta",
                table: "Roubos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_Bicicletas_BicicletaId",
                table: "Alertas",
                column: "BicicletaId",
                principalTable: "Bicicletas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_TiposAlertas_IdTipoAlerta",
                table: "Alertas",
                column: "IdTipoAlerta",
                principalTable: "TiposAlertas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_Usuarios_UsuarioGeradorId",
                table: "Alertas",
                column: "UsuarioGeradorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Bicicletas_IdBicicleta",
                table: "Arquivos",
                column: "IdBicicleta",
                principalTable: "Bicicletas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Roubos_IdRoubo",
                table: "Arquivos",
                column: "IdRoubo",
                principalTable: "Roubos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Enderecos_IdEndereco",
                table: "Bicicletas",
                column: "IdEndereco",
                principalTable: "Enderecos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Usuarios_IdUsuario",
                table: "Bicicletas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_IdUsuario",
                table: "Enderecos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roubos_Bicicletas_IdBicicleta",
                table: "Roubos",
                column: "IdBicicleta",
                principalTable: "Bicicletas",
                principalColumn: "Id");
        }
    }
}
