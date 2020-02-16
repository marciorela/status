using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Status.Data.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataInc = table.Column<DateTime>(nullable: false),
                    DataAlt = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servidores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataInc = table.Column<DateTime>(nullable: false),
                    DataAlt = table.Column<DateTime>(nullable: false),
                    Host = table.Column<string>(maxLength: 100, nullable: false),
                    CheckInterval = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servidores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortasServidor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataInc = table.Column<DateTime>(nullable: false),
                    DataAlt = table.Column<DateTime>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    ServidorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortasServidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortasServidor_Servidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortasServidor_ServidorId",
                table: "PortasServidor",
                column: "ServidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Servidores_UsuarioId",
                table: "Servidores",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortasServidor");

            migrationBuilder.DropTable(
                name: "Servidores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
