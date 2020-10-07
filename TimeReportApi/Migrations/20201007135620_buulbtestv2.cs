using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeReportApi.Migrations
{
    public partial class buulbtestv2 : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_missions_customers_customerId",
                table: "missions");

            migrationBuilder.DropForeignKey(
                name: "FK_missions_users_userId",
                table: "missions");

            migrationBuilder.DropForeignKey(
                name: "FK_registries_tasks_taskId",
                table: "registries");

            migrationBuilder.DropForeignKey(
                name: "FK_registries_users_userId",
                table: "registries");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_missions_missionId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_registries",
                table: "registries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_missions",
                table: "missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favoriteMissions",
                table: "favoriteMissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "registries",
                newName: "Registries");

            migrationBuilder.RenameTable(
                name: "missions",
                newName: "Missions");

            migrationBuilder.RenameTable(
                name: "missionMembers",
                newName: "MissionMembers");

            migrationBuilder.RenameTable(
                name: "favoriteMissions",
                newName: "FavoriteMissions");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "eMail",
                table: "Users",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Tasks",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Tasks",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "missionId",
                table: "Tasks",
                newName: "MissionId");

            migrationBuilder.RenameColumn(
                name: "invoice",
                table: "Tasks",
                newName: "Invoice");

            migrationBuilder.RenameColumn(
                name: "finished",
                table: "Tasks",
                newName: "Finished");

            migrationBuilder.RenameColumn(
                name: "estimatedHour",
                table: "Tasks",
                newName: "EstimatedHour");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Tasks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Tasks",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "actualHours",
                table: "Tasks",
                newName: "ActualHours");

            migrationBuilder.RenameColumn(
                name: "taskId",
                table: "Tasks",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_userId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_missionId",
                table: "Tasks",
                newName: "IX_Tasks_MissionId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Registries",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "taskId",
                table: "Registries",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "invoice",
                table: "Registries",
                newName: "Invoice");

            migrationBuilder.RenameColumn(
                name: "hours",
                table: "Registries",
                newName: "Hours");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Registries",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Registries",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "registryId",
                table: "Registries",
                newName: "RegistryId");

            migrationBuilder.RenameIndex(
                name: "IX_registries_userId",
                table: "Registries",
                newName: "IX_Registries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_registries_taskId",
                table: "Registries",
                newName: "IX_Registries_TaskId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Missions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Missions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Missions",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "missionName",
                table: "Missions",
                newName: "MissionName");

            migrationBuilder.RenameColumn(
                name: "finished",
                table: "Missions",
                newName: "Finished");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Missions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Missions",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Missions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "missionId",
                table: "Missions",
                newName: "MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_missions_userId",
                table: "Missions",
                newName: "IX_Missions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_missions_customerId",
                table: "Missions",
                newName: "IX_Missions_CustomerId");

            migrationBuilder.RenameColumn(
                name: "missionId",
                table: "MissionMembers",
                newName: "MissionId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "MissionMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_missionMembers_missionId",
                table: "MissionMembers",
                newName: "IX_MissionMembers_MissionId");

            migrationBuilder.RenameColumn(
                name: "missionId",
                table: "FavoriteMissions",
                newName: "MissionId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "FavoriteMissions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_favoriteMissions_missionId",
                table: "FavoriteMissions",
                newName: "IX_FavoriteMissions_MissionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Customers",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Missions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registries",
                table: "Registries",
                column: "RegistryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Missions",
                table: "Missions",
                column: "MissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionMembers",
                table: "MissionMembers",
                columns: new[] { "UserId", "MissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteMissions",
                table: "FavoriteMissions",
                columns: new[] { "UserId", "MissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "MissionId",
                keyValue: 1,
                column: "Color",
                value: "#F0D87B");

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "MissionId",
                keyValue: 2,
                column: "Color",
                value: "#5B8D76");

            migrationBuilder.InsertData(
                table: "Missions",
                columns: new[] { "MissionId", "Color", "Created", "CustomerId", "Description", "Finished", "MissionName", "Start", "Status", "UserId" },
                values: new object[,]
                {
                    { 3, "#E26B9D", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Make sparkles", null, "sparkles", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 4, "#F08B7B", new DateTime(2030, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Make website now", null, "make website", new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 5, "#7BB6F0", new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "no", null, "yes", new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Registries",
                keyColumn: "RegistryId",
                keyValue: 3,
                column: "TaskId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Registries",
                keyColumn: "RegistryId",
                keyValue: 4,
                column: "TaskId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMissions_Missions_MissionId",
                table: "FavoriteMissions",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMissions_Users_UserId",
                table: "FavoriteMissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionMembers_Missions_MissionId",
                table: "MissionMembers",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionMembers_Users_UserId",
                table: "MissionMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Customers_CustomerId",
                table: "Missions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Tasks_TaskId",
                table: "Registries",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Users_UserId",
                table: "Registries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Missions_MissionId",
                table: "Tasks",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMissions_Missions_MissionId",
                table: "FavoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMissions_Users_UserId",
                table: "FavoriteMissions");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionMembers_Missions_MissionId",
                table: "MissionMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionMembers_Users_UserId",
                table: "MissionMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Customers_CustomerId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Tasks_TaskId",
                table: "Registries");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Users_UserId",
                table: "Registries");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Missions_MissionId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registries",
                table: "Registries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Missions",
                table: "Missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionMembers",
                table: "MissionMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteMissions",
                table: "FavoriteMissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "MissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "MissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "MissionId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Missions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tasks");

            migrationBuilder.RenameTable(
                name: "Registries",
                newName: "registries");

            migrationBuilder.RenameTable(
                name: "Missions",
                newName: "missions");

            migrationBuilder.RenameTable(
                name: "MissionMembers",
                newName: "missionMembers");

            migrationBuilder.RenameTable(
                name: "FavoriteMissions",
                newName: "favoriteMissions");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "users",
                newName: "eMail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tasks",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "tasks",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "tasks",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tasks",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "tasks",
                newName: "missionId");

            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "tasks",
                newName: "invoice");

            migrationBuilder.RenameColumn(
                name: "Finished",
                table: "tasks",
                newName: "finished");

            migrationBuilder.RenameColumn(
                name: "EstimatedHour",
                table: "tasks",
                newName: "estimatedHour");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tasks",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "tasks",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "ActualHours",
                table: "tasks",
                newName: "actualHours");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "tasks",
                newName: "taskId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "tasks",
                newName: "IX_tasks_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_MissionId",
                table: "tasks",
                newName: "IX_tasks_missionId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "registries",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "registries",
                newName: "taskId");

            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "registries",
                newName: "invoice");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "registries",
                newName: "hours");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "registries",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "registries",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "RegistryId",
                table: "registries",
                newName: "registryId");

            migrationBuilder.RenameIndex(
                name: "IX_Registries_UserId",
                table: "registries",
                newName: "IX_registries_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Registries_TaskId",
                table: "registries",
                newName: "IX_registries_taskId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "missions",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "missions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "missions",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "MissionName",
                table: "missions",
                newName: "missionName");

            migrationBuilder.RenameColumn(
                name: "Finished",
                table: "missions",
                newName: "finished");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "missions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "missions",
                newName: "customerId");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "missions",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "missions",
                newName: "missionId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_UserId",
                table: "missions",
                newName: "IX_missions_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_CustomerId",
                table: "missions",
                newName: "IX_missions_customerId");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "missionMembers",
                newName: "missionId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "missionMembers",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionMembers_MissionId",
                table: "missionMembers",
                newName: "IX_missionMembers_missionId");

            migrationBuilder.RenameColumn(
                name: "MissionId",
                table: "favoriteMissions",
                newName: "missionId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "favoriteMissions",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteMissions_MissionId",
                table: "favoriteMissions",
                newName: "IX_favoriteMissions_missionId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "customers",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "customers",
                newName: "customerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks",
                table: "tasks",
                column: "taskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_registries",
                table: "registries",
                column: "registryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_missions",
                table: "missions",
                column: "missionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_missionMembers",
                table: "missionMembers",
                columns: new[] { "userId", "missionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_favoriteMissions",
                table: "favoriteMissions",
                columns: new[] { "userId", "missionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "customerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_missions_customers_customerId",
                table: "missions",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "customerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_missions_users_userId",
                table: "missions",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_registries_tasks_taskId",
                table: "registries",
                column: "taskId",
                principalTable: "tasks",
                principalColumn: "taskId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_registries_users_userId",
                table: "registries",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_missions_missionId",
                table: "tasks",
                column: "missionId",
                principalTable: "missions",
                principalColumn: "missionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_users_userId",
                table: "tasks",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
