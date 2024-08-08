﻿// <auto-generated />
using DesafioActivex.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioActivex.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DesafioActivex.Models.Course", b =>
                {
                    b.Property<string>("CourseID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CourseID");

                    b.ToTable("Course");

                    b.HasData(
                        new
                        {
                            CourseID = "C001",
                            Name = "Curso A"
                        },
                        new
                        {
                            CourseID = "C002",
                            Name = "Curso B"
                        },
                        new
                        {
                            CourseID = "C003",
                            Name = "Curso C"
                        });
                });

            modelBuilder.Entity("DesafioActivex.Models.Student", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentID");

                    b.HasIndex("CourseID");

                    b.ToTable("Student");

                    b.HasData(
                        new
                        {
                            StudentID = "S001",
                            CourseID = "C001",
                            Email = "alunoA@mail.com",
                            Name = "Aluno A"
                        },
                        new
                        {
                            StudentID = "S002",
                            CourseID = "C001",
                            Email = "alunoB@mail.com",
                            Name = "Aluno B"
                        },
                        new
                        {
                            StudentID = "S003",
                            CourseID = "C002",
                            Email = "alunoC@mail.com",
                            Name = "Aluno C"
                        });
                });

            modelBuilder.Entity("DesafioActivex.Models.Student", b =>
                {
                    b.HasOne("DesafioActivex.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DesafioActivex.Models.Course", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
