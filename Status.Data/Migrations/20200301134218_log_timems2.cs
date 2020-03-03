using Microsoft.EntityFrameworkCore.Migrations;

namespace Status.Data.Migrations
{
    public partial class log_timems2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TimeMS",
                table: "LogsChecked",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeMS",
                table: "LogsChecked");
        }
    }
}
