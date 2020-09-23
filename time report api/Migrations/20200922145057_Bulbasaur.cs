using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace time_report_api.Migrations
{
    public partial class Bulbasaur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "finished",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "missions");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "registries",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "finished",
                table: "missions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "registries");

            migrationBuilder.AddColumn<DateTime>(
                name: "finished",
                table: "tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "finished",
                table: "missions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "owner",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
