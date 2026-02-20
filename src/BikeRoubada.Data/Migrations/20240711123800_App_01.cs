using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace BikeRoubada.Data.Migrations
{
    /// <inheritdoc />
    public partial class App_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposAlertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAlertas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdentificadorPessoal = table.Column<string>(type: "varchar(14)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genero = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Rua = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Principal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bicicletas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Serial = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(300)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocalizacaoCadastro = table.Column<Point>(type: "point", nullable: false),
                    IdEndereco = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicicletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bicicletas_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bicicletas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdBicicleta = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BicicletaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Localizacao = table.Column<Point>(type: "point", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdUsuarioGerador = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioGeradorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IdTipoAlerta = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Bicicletas_BicicletaId",
                        column: x => x.BicicletaId,
                        principalTable: "Bicicletas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alertas_TiposAlertas_IdTipoAlerta",
                        column: x => x.IdTipoAlerta,
                        principalTable: "TiposAlertas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alertas_Usuarios_UsuarioGeradorId",
                        column: x => x.UsuarioGeradorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roubos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Relato = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataRoubo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataRecuperacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Localizacao = table.Column<Point>(type: "point", nullable: false),
                    NumeroBoletim = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdBicicleta = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roubos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roubos_Bicicletas_IdBicicleta",
                        column: x => x.IdBicicleta,
                        principalTable: "Bicicletas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NomeArquivo = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdRoubo = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IdBicicleta = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    BicicletaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivos_Bicicletas_BicicletaId",
                        column: x => x.BicicletaId,
                        principalTable: "Bicicletas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arquivos_Roubos_IdRoubo",
                        column: x => x.IdRoubo,
                        principalTable: "Roubos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_BicicletaId",
                table: "Alertas",
                column: "BicicletaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_IdTipoAlerta",
                table: "Alertas",
                column: "IdTipoAlerta");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UsuarioGeradorId",
                table: "Alertas",
                column: "UsuarioGeradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_BicicletaId",
                table: "Arquivos",
                column: "BicicletaId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_IdRoubo",
                table: "Arquivos",
                column: "IdRoubo");

            migrationBuilder.CreateIndex(
                name: "IX_Bicicletas_IdEndereco",
                table: "Bicicletas",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Bicicletas_IdUsuario",
                table: "Bicicletas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_IdUsuario",
                table: "Enderecos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Roubos_IdBicicleta",
                table: "Roubos",
                column: "IdBicicleta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "TiposAlertas");

            migrationBuilder.DropTable(
                name: "Roubos");

            migrationBuilder.DropTable(
                name: "Bicicletas");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
