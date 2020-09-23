using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class BulbasaurSeedv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "invoice",
                table: "registries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invoice",
                table: "registries");
        }
    }
}
