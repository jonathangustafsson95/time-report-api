using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class i : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    userId = table.Column<int>(nullable: false),
                    missionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_missionMembers", x => new { x.userId, x.missionId });
                });

            migrationBuilder.CreateTable(
                name: "missions",
                columns: table => new
                {
                    missionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    customerId = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    start = table.Column<DateTime>(nullable: false),
                    finished = table.Column<DateTime>(nullable: true),
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
                    registryId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_registries", x => x.registryId);
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
                    start = table.Column<DateTime>(nullable: false),
                    finished = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "customerId", "created", "name" },
                values: new object[] { 1, new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bobby" });

            migrationBuilder.InsertData(
                table: "favoriteMissions",
                columns: new[] { "userId", "missionId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "missionMembers",
                columns: new[] { "userId", "missionId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "missions",
                columns: new[] { "missionId", "created", "customerId", "description", "finished", "start", "status", "userId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Make cool stuffs", null, new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2020, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lorem Ipsum ", new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "registries",
                columns: new[] { "registryId", "created", "date", "hours", "invoice", "taskId", "userId" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8.0, 0, 1, 1 });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "taskId", "userId", "actualHours", "created", "description", "estimatedHour", "finished", "invoice", "missionId", "name", "start", "status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Make cool thing work", 8.3000000000000007, null, 0, 1, "work", new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, 1, null, new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PLACEHOLDER", 8.3000000000000007, new DateTime(2020, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "PLACEHOLDER", new DateTime(2020, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "userId", "eMail", "password", "userName" },
                values: new object[] { 1, "hej@lol.com", "abc123", "John" });
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
