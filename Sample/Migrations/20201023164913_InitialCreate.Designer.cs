﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.DbContexts;

namespace Sample.Migrations
{
    [DbContext(typeof(SampleContext))]
    [Migration("20201023164913_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sample.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Activity")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfStaff")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"),
                            Activity = "web developing",
                            DateTime = new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 0, DateTimeKind.Unspecified).AddTicks(7772), new TimeSpan(0, 3, 30, 0, 0)),
                            Name = "Best Company For Ever",
                            NumberOfStaff = 10
                        },
                        new
                        {
                            Id = new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"),
                            Activity = "developing software",
                            DateTime = new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(4629), new TimeSpan(0, 3, 30, 0, 0)),
                            Name = "Microsoft",
                            NumberOfStaff = 400
                        },
                        new
                        {
                            Id = new Guid("c8e7f681-fa1d-4e96-a897-d19439441e35"),
                            Activity = "Hardware Manufacturing",
                            DateTime = new DateTimeOffset(new DateTime(2020, 10, 23, 20, 19, 12, 14, DateTimeKind.Unspecified).AddTicks(5105), new TimeSpan(0, 3, 30, 0, 0)),
                            Name = "Huawei",
                            NumberOfStaff = 100
                        });
                });

            modelBuilder.Entity("Sample.Entities.JobPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobPositions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b01bf89a-8125-4518-93e9-6c826b3f4d73"),
                            CompanyId = new Guid("e4978846-3eec-432d-b54d-db08b75a2a83"),
                            Degree = 1,
                            Description = ".experienced with entity frame work,.good knowledge of sql server",
                            Title = ".Net Developer"
                        },
                        new
                        {
                            Id = new Guid("c8edaad1-c78f-4034-a447-130d12a2d59e"),
                            CompanyId = new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"),
                            Degree = 2,
                            Description = ".having good knowledge of IIS",
                            Title = "Dev op"
                        },
                        new
                        {
                            Id = new Guid("ce159fbb-84aa-4d8a-aaf5-ec1fb727a664"),
                            CompanyId = new Guid("8702b207-d45e-4328-ae36-0216b95e19f5"),
                            Degree = 0,
                            Description = ".Experienced with CSS3 and Html5",
                            Title = "Front End Developer"
                        });
                });

            modelBuilder.Entity("Sample.Entities.JobPosition", b =>
                {
                    b.HasOne("Sample.Entities.Company", "Company")
                        .WithMany("JobPositions")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}