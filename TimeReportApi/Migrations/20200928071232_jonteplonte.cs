using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeReportApi.Migrations
{
    public partial class jonteplonte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favoriteMissions_missions_missionId",
                table: "favoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_favoriteMissions_users_userId",
                table: "favoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_missionMembers_missions_missionId",
                table: "missionMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_missionMembers_users_userId",
                table: "missionMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_favoriteMissions_missions_missionId",
                table: "favoriteMissions",
                column: "missionId",
                principalTable: "missions",
                principalColumn: "missionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_favoriteMissions_users_userId",
                table: "favoriteMissions",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_missionMembers_missions_missionId",
                table: "missionMembers",
                column: "missionId",
                principalTable: "missions",
                principalColumn: "missionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_missionMembers_users_userId",
                table: "missionMembers",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favoriteMissions_missions_missionId",
                table: "favoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_favoriteMissions_users_userId",
                table: "favoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_missionMembers_missions_missionId",
                table: "missionMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_missionMembers_users_userId",
                table: "missionMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_favoriteMissions_missions_missionId",
                table: "favoriteMissions",
                column: "missionId",
                principalTable: "missions",
                principalColumn: "missionId");

            migrationBuilder.AddForeignKey(
                name: "FK_favoriteMissions_users_userId",
                table: "favoriteMissions",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_missionMembers_missions_missionId",
                table: "missionMembers",
                column: "missionId",
                principalTable: "missions",
                principalColumn: "missionId");

            migrationBuilder.AddForeignKey(
                name: "FK_missionMembers_users_userId",
                table: "missionMembers",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId");
        }
    }
}
