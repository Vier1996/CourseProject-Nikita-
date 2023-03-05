﻿// <auto-generated />
using CourseProject.Codebase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseProject.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20230305135337_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CourseProject.Codebase.Context.FormedEducationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FormName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FormedEducations");
                });

            modelBuilder.Entity("CourseProject.Codebase.Context.GroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountOfStudents")
                        .HasColumnType("int");

                    b.Property<int>("CountOfSubGroups")
                        .HasColumnType("int");

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<string>("Faculty")
                        .HasColumnType("longtext");

                    b.Property<int>("FormedEducationReferenceId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .HasColumnType("longtext");

                    b.Property<int>("QualificationReferenceId")
                        .HasColumnType("int");

                    b.Property<int>("SpecialityReferenceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormedEducationReferenceId");

                    b.HasIndex("QualificationReferenceId");

                    b.HasIndex("SpecialityReferenceId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CourseProject.Codebase.Context.QualificationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("QualificationName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("CourseProject.Codebase.Context.SpecialityModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Profile")
                        .HasColumnType("longtext");

                    b.Property<string>("SpecialityName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("CourseProject.Codebase.Context.GroupModel", b =>
                {
                    b.HasOne("CourseProject.Codebase.Context.FormedEducationModel", "FormedEducationReference")
                        .WithMany()
                        .HasForeignKey("FormedEducationReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseProject.Codebase.Context.QualificationModel", "QualificationReference")
                        .WithMany()
                        .HasForeignKey("QualificationReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseProject.Codebase.Context.SpecialityModel", "SpecialityReference")
                        .WithMany()
                        .HasForeignKey("SpecialityReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormedEducationReference");

                    b.Navigation("QualificationReference");

                    b.Navigation("SpecialityReference");
                });
#pragma warning restore 612, 618
        }
    }
}