﻿// <auto-generated />
using System;
using DataAccessLayer;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace time_report_api.Migrations
{
    [DbContext(typeof(BulbasaurContext))]
    [Migration("20200923071524_BulbasaurSeedv2")]
    partial class BulbasaurSeedv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Model.Customer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customerId");

                    b.ToTable("customers");

                    b.HasData(
                        new
                        {
                            customerId = 1,
                            created = new DateTime(2020, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Bobby"
                        });
                });

            modelBuilder.Entity("Data.Model.FavoriteMission", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<int>("missionId")
                        .HasColumnType("int");

                    b.HasKey("userId", "missionId");

                    b.ToTable("favoriteMissions");

                    b.HasData(
                        new
                        {
                            userId = 1,
                            missionId = 1
                        });
                });

            modelBuilder.Entity("Data.Model.Mission", b =>
                {
                    b.Property<int>("missionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("finished")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("missionId");

                    b.ToTable("missions");

                    b.HasData(
                        new
                        {
                            missionId = 1,
                            created = new DateTime(2020, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            customerId = 1,
                            description = "Make cool stuffs",
                            start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 1,
                            userId = 1
                        },
                        new
                        {
                            missionId = 2,
                            created = new DateTime(2020, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            customerId = 1,
                            description = "Lorem Ipsum ",
                            finished = new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 1,
                            userId = 1
                        });
                });

            modelBuilder.Entity("Data.Model.MissionMember", b =>
                {
                    b.Property<int>("missionMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("missionMemberId");

                    b.ToTable("missionMembers");

                    b.HasData(
                        new
                        {
                            missionMemberId = 1,
                            userId = 1
                        });
                });

            modelBuilder.Entity("Data.Model.Registry", b =>
                {
                    b.Property<int>("reqistryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("hours")
                        .HasColumnType("float");

                    b.Property<int>("invoice")
                        .HasColumnType("int");

                    b.Property<int>("taskId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("reqistryId");

                    b.ToTable("registries");

                    b.HasData(
                        new
                        {
                            reqistryId = 1,
                            created = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            date = new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            hours = 8.0,
                            invoice = 0,
                            taskId = 1,
                            userId = 1
                        });
                });

            modelBuilder.Entity("Data.Model.Task", b =>
                {
                    b.Property<int>("taskId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<double?>("actualHours")
                        .HasColumnType("float");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("estimatedHour")
                        .HasColumnType("float");

                    b.Property<DateTime?>("finished")
                        .HasColumnType("datetime2");

                    b.Property<int>("invoice")
                        .HasColumnType("int");

                    b.Property<int>("missionId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("taskId", "userId");

                    b.ToTable("tasks");

                    b.HasData(
                        new
                        {
                            taskId = 1,
                            userId = 1,
                            created = new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "Make cool thing work",
                            estimatedHour = 8.3000000000000007,
                            invoice = 0,
                            missionId = 1,
                            name = "work",
                            start = new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0
                        },
                        new
                        {
                            taskId = 2,
                            userId = 1,
                            created = new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "PLACEHOLDER",
                            estimatedHour = 8.3000000000000007,
                            finished = new DateTime(2020, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            invoice = 1,
                            missionId = 1,
                            name = "PLACEHOLDER",
                            start = new DateTime(2020, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0
                        });
                });

            modelBuilder.Entity("Data.Model.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("eMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            userId = 1,
                            eMail = "hej@lol.com",
                            password = "abc123",
                            userName = "John"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
