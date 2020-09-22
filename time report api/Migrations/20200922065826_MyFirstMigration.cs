using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    custNo = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "favoriteMissions",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false),
                    missionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoriteMissions", x => new { x.userId, x.missionId });
                });

            migrationBuilder.CreateTable(
                name: "missionMembers",
                columns: table => new
                {
                    missionMemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_missionMembers", x => x.missionMemberId);
                });

            migrationBuilder.CreateTable(
                name: "missions",
                columns: table => new
                {
                    missionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    custNo = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    owner = table.Column<int>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    finished = table.Column<DateTime>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_missions", x => x.missionId);
                });

            migrationBuilder.CreateTable(
                name: "registries",
                columns: table => new
                {
                    reqistryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    hours = table.Column<double>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    invoice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registries", x => x.reqistryId);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    taskId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    missionId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    estimatedHour = table.Column<double>(nullable: false),
                    actualHours = table.Column<double>(nullable: true),
                    invoice = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    finished = table.Column<DateTime>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => new { x.taskId, x.userId });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    eMail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "favoriteMissions");

            migrationBuilder.DropTable(
                name: "missionMembers");

            migrationBuilder.DropTable(
                name: "missions");

            migrationBuilder.DropTable(
                name: "registries");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
