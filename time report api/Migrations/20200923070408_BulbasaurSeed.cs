using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class BulbasaurSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invoice",
                table: "registries");

            migrationBuilder.DropColumn(
                name: "custNo",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "custNo",
                table: "customers");

            migrationBuilder.AddColumn<DateTime>(
                name: "finished",
                table: "tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "customerId",
                table: "missions",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "missionMemberId", "userId" },
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
                columns: new[] { "reqistryId", "created", "date", "description", "hours", "taskId", "userId" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8.0, 1, 1 });

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
            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "customerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "favoriteMissions",
                keyColumns: new[] { "userId", "missionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "missionMembers",
                keyColumn: "missionMemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "missions",
                keyColumn: "missionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "missions",
                keyColumn: "missionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "registries",
                keyColumn: "reqistryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumns: new[] { "taskId", "userId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumns: new[] { "taskId", "userId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "userId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "finished",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "missions");

            migrationBuilder.AddColumn<int>(
                name: "invoice",
                table: "registries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "custNo",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "custNo",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
