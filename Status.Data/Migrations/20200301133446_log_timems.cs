using Microsoft.EntityFrameworkCore.Migrations;

namespace Status.Data.Migrations
{
    public partial class log_timems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortNumber",
                table: "LogsChecked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortNumber",
                table: "LogsChecked",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
