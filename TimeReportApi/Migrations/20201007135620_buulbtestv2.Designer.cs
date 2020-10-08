﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TimeReportApi.Migrations
{
    [DbContext(typeof(BulbasaurDevContext))]
    [Migration("20201007135620_buulbtestv2")]
    partial class buulbtestv2
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
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Created = new DateTime(2020, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bobby"
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.FavoriteMission", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MissionId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MissionId");

                    b.HasIndex("MissionId");

                    b.ToTable("FavoriteMissions");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            MissionId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Mission", b =>
                {
                    b.Property<int>("MissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("datetime2");

                    b.Property<string>("MissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MissionId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Missions");

                    b.HasData(
                        new
                        {
                            MissionId = 1,
                            Color = "#F0D87B",
                            Created = new DateTime(2020, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            Description = "Make cool stuffs",
                            MissionName = "Operation Cool Stuffs",
                            Start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1,
                            UserId = 1
                        },
                        new
                        {
                            MissionId = 2,
                            Color = "#5B8D76",
                            Created = new DateTime(2020, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            Description = "Lorem Ipsum ",
                            Finished = new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MissionName = "dolor sit amet",
                            Start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1,
                            UserId = 1
                        },
                        new
                        {
                            MissionId = 3,
                            Color = "#E26B9D",
                            Created = new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            Description = "Make sparkles",
                            MissionName = "sparkles",
                            Start = new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1,
                            UserId = 1
                        },
                        new
                        {
                            MissionId = 4,
                            Color = "#F08B7B",
                            Created = new DateTime(2030, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            Description = "Make website now",
                            MissionName = "make website",
                            Start = new DateTime(2020, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1,
                            UserId = 1
                        },
                        new
                        {
                            MissionId = 5,
                            Color = "#7BB6F0",
                            Created = new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            Description = "no",
                            MissionName = "yes",
                            Start = new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.MissionMember", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MissionId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MissionId");

                    b.HasIndex("MissionId");

                    b.ToTable("MissionMembers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            MissionId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Registry", b =>
                {
                    b.Property<int>("RegistryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Hours")
                        .HasColumnType("float");

                    b.Property<int>("Invoice")
                        .HasColumnType("int");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RegistryId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Registries");

                    b.HasData(
                        new
                        {
                            RegistryId = 1,
                            Created = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 8.0,
                            Invoice = 0,
                            TaskId = 1,
                            UserId = 1
                        },
                        new
                        {
                            RegistryId = 2,
                            Created = new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 8.0,
                            Invoice = 0,
                            TaskId = 1,
                            UserId = 1
                        },
                        new
                        {
                            RegistryId = 3,
                            Created = new DateTime(2021, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2020, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 8.0,
                            Invoice = 0,
                            TaskId = 1,
                            UserId = 1
                        },
                        new
                        {
                            RegistryId = 4,
                            Created = new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2020, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 8.0,
                            Invoice = 0,
                            TaskId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("ActualHours")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("EstimatedHour")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("datetime2");

                    b.Property<int>("Invoice")
                        .HasColumnType("int");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TaskId");

                    b.HasIndex("MissionId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            TaskId = 1,
                            Created = new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Make cool thing work",
                            EstimatedHour = 8.3000000000000007,
                            Invoice = 0,
                            MissionId = 1,
                            Name = "work",
                            Start = new DateTime(2020, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            UserId = 1
                        },
                        new
                        {
                            TaskId = 2,
                            Created = new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "PLACEHOLDER",
                            EstimatedHour = 8.3000000000000007,
                            Finished = new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Invoice = 1,
                            MissionId = 1,
                            Name = "PLACEHOLDER",
                            Start = new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            UserId = 1
                        },
                        new
                        {
                            TaskId = 3,
                            Created = new DateTime(2020, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "PLACEHOLDER",
                            EstimatedHour = 8.3000000000000007,
                            Finished = new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Invoice = 1,
                            MissionId = 1,
                            Name = "PLACEHOLDER",
                            Start = new DateTime(2020, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            UserId = 1
                        },
                        new
                        {
                            TaskId = 4,
                            Created = new DateTime(2020, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "PLACEHOLDER",
                            EstimatedHour = 8.3000000000000007,
                            Finished = new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Invoice = 1,
                            MissionId = 1,
                            Name = "PLACEHOLDER",
                            Start = new DateTime(2020, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EMail = "hej@lol.com",
                            Password = "abc123",
                            UserName = "John"
                        });
                });

            modelBuilder.Entity("CommonLibrary.Model.FavoriteMission", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany("FavoritedMission")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany("MissionFavorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLibrary.Model.Mission", b =>
                {
                    b.HasOne("CommonLibrary.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CommonLibrary.Model.MissionMember", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany("MissionMembers")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany("MissionMemberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonLibrary.Model.Registry", b =>
                {
                    b.HasOne("CommonLibrary.Model.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CommonLibrary.Model.Task", b =>
                {
                    b.HasOne("CommonLibrary.Model.Mission", "Mission")
                        .WithMany()
                        .HasForeignKey("MissionId");

                    b.HasOne("CommonLibrary.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
