﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace time_report_api.Migrations
{
    [DbContext(typeof(BulbasaurDevContext))]
    [Migration("20200928081250_dbtest")]
    partial class dbtest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLibrary.Model.Customer", b =>
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
                            created = new DateTime(2020, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Bobby"
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.FavoriteMission", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<int>("missionId")
                        .HasColumnType("int");

                    b.HasKey("userId", "missionId");

                    b.HasIndex("missionId");

                    b.ToTable("favoriteMissions");

                    b.HasData(
                        new
                        {
                            userId = 1,
                            missionId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Mission", b =>
                {
                    b.Property<int>("missionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("customerId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("finished")
                        .HasColumnType("datetime2");

                    b.Property<string>("missionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("missionId");

                    b.HasIndex("customerId");

                    b.HasIndex("userId");

                    b.ToTable("missions");

                    b.HasData(
                        new
                        {
                            missionId = 1,
                            created = new DateTime(2020, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            customerId = 1,
                            description = "Make cool stuffs",
                            missionName = "Operation Cool Stuffs",
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
                            missionName = "dolor sit amet",
                            start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 1,
                            userId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.MissionMember", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<int>("missionId")
                        .HasColumnType("int");

                    b.HasKey("userId", "missionId");

                    b.HasIndex("missionId");

                    b.ToTable("missionMembers");

                    b.HasData(
                        new
                        {
                            userId = 1,
                            missionId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Registry", b =>
                {
                    b.Property<int>("registryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<double>("hours")
                        .HasColumnType("float");

                    b.Property<int>("invoice")
                        .HasColumnType("int");

                    b.Property<int?>("taskId")
                        .HasColumnType("int");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("registryId");

                    b.HasIndex("taskId");

                    b.HasIndex("userId");

                    b.ToTable("registries");

                    b.HasData(
                        new
                        {
                            registryId = 1,
                            created = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            date = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            hours = 8.0,
                            invoice = 0,
                            taskId = 1,
                            userId = 1
                        },
                        new
                        {
                            registryId = 2,
                            created = new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            date = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            hours = 8.0,
                            invoice = 0,
                            taskId = 1,
                            userId = 1
                        },
                        new
                        {
                            registryId = 3,
                            created = new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            date = new DateTime(2020, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            hours = 8.0,
                            invoice = 0,
                            taskId = 1,
                            userId = 1
                        },
                        new
                        {
                            registryId = 4,
                            created = new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            date = new DateTime(2020, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            hours = 8.0,
                            invoice = 0,
                            taskId = 1,
                            userId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Task", b =>
                {
                    b.Property<int>("taskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int?>("missionId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("taskId");

                    b.HasIndex("missionId");

                    b.HasIndex("userId");

                    b.ToTable("tasks");

                    b.HasData(
                        new
                        {
                            taskId = 1,
                            created = new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "Make cool thing work",
                            estimatedHour = 8.3000000000000007,
                            invoice = 0,
                            missionId = 1,
                            name = "work",
                            start = new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0,
                            userId = 1
                        },
                        new
                        {
                            taskId = 2,
                            created = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "PLACEHOLDER",
                            estimatedHour = 8.3000000000000007,
                            finished = new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            invoice = 1,
                            missionId = 1,
                            name = "PLACEHOLDER",
                            start = new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0,
                            userId = 1
                        },
                        new
                        {
                            taskId = 3,
                            created = new DateTime(2020, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "PLACEHOLDER",
                            estimatedHour = 8.3000000000000007,
                            finished = new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            invoice = 1,
                            missionId = 1,
                            name = "PLACEHOLDER",
                            start = new DateTime(2020, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0,
                            userId = 1
                        },
                        new
                        {
                            taskId = 4,
                            created = new DateTime(2020, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "PLACEHOLDER",
                            estimatedHour = 8.3000000000000007,
                            finished = new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            invoice = 1,
                            missionId = 1,
                            name = "PLACEHOLDER",
                            start = new DateTime(2020, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            status = 0,
                            userId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.User", b =>
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

            modelBuilder.Entity("CommonLibrary.Model.FavoriteMission", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany("favoritedMission")
                        .HasForeignKey("missionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany("missionFavorites")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLibrary.Model.Mission", b =>
                {
                    b.HasOne("CommonLibrary.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("customerId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("CommonLibrary.Model.MissionMember", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany("missionMembers")
                        .HasForeignKey("missionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany("missionMemberships")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLibrary.Model.Registry", b =>
                {
                    b.HasOne("CommonLibrary.Model.Task", "Task")
                        .WithMany()
                        .HasForeignKey("taskId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("CommonLibrary.Model.Task", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany()
                        .HasForeignKey("missionId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}
