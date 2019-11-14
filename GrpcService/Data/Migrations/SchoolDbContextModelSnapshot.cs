﻿// <auto-generated />
using GrpcService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GrpcService.Data.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrpcService.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("School")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 11,
                            FirstName = "Ann",
                            LastName = "Fox",
                            School = "Nursing"
                        },
                        new
                        {
                            StudentId = 22,
                            FirstName = "Sam",
                            LastName = "Doe",
                            School = "Mining"
                        },
                        new
                        {
                            StudentId = 33,
                            FirstName = "Sue",
                            LastName = "Cox",
                            School = "Business"
                        },
                        new
                        {
                            StudentId = 44,
                            FirstName = "Tim",
                            LastName = "Lee",
                            School = "Computing"
                        },
                        new
                        {
                            StudentId = 55,
                            FirstName = "Jan",
                            LastName = "Ray",
                            School = "Computing"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
