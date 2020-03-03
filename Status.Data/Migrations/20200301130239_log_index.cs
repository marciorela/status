using Microsoft.EntityFrameworkCore.Migrations;

namespace Status.Data.Migrations
{
    public partial class log_index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogsChecked_DateTimeChecked_PortId",
                table: "LogsChecked",
                columns: new[] { "DateTimeChecked", "PortId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogsChecked_DateTimeChecked_PortId",
                table: "LogsChecked");
        }
    }
}
