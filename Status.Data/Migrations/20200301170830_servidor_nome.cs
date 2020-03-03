using Microsoft.EntityFrameworkCore.Migrations;

namespace Status.Data.Migrations
{
    public partial class servidor_nome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Servidores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Servidores");
        }
    }
}
