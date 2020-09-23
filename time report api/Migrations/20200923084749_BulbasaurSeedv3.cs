using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class BulbasaurSeedv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers");

            migrationBuilder.DeleteData(
                table: "missionMembers",
                keyColumn: "missionMemberId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "missionMemberId",
                table: "missionMembers");

            migrationBuilder.AddColumn<int>(
                name: "missionId",
                table: "missionMembers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers",
                columns: new[] { "userId", "missionId" });

            migrationBuilder.InsertData(
                table: "missionMembers",
                columns: new[] { "userId", "missionId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers");

            migrationBuilder.DeleteData(
                table: "missionMembers",
                keyColumns: new[] { "userId", "missionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DropColumn(
                name: "missionId",
                table: "missionMembers");

            migrationBuilder.AddColumn<int>(
                name: "missionMemberId",
                table: "missionMembers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers",
                column: "missionMemberId");

            migrationBuilder.InsertData(
                table: "missionMembers",
                columns: new[] { "missionMemberId", "userId" },
                values: new object[] { 1, 1 });
        }
    }
}
