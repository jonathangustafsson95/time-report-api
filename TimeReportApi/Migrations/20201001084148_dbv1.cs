using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeReportApi.Migrations
{
    public partial class dbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "registryId",
                keyValue: 3,
                column: "taskId",
                value: null);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "registryId",
                keyValue: 4,
                column: "taskId",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "registryId",
                keyValue: 3,
                column: "taskId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "registries",
                keyColumn: "registryId",
                keyValue: 4,
                column: "taskId",
                value: 1);
        }
    }
}
