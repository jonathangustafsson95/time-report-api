using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class db11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "missionName",
                table: "missions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "missions",
                keyColumn: "missionId",
                keyValue: 1,
                column: "missionName",
                value: "Operation Cool Stuffs");

            migrationBuilder.UpdateData(
                table: "missions",
                keyColumn: "missionId",
                keyValue: 2,
                column: "missionName",
                value: "dolor sit amet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "missionName",
                table: "missions");
        }
    }
}
