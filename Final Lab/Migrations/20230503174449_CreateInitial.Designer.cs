﻿// <auto-generated />
using System;
using Final_Lab.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Final_Lab.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20230503174449_CreateInitial")]
    partial class CreateInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Final_Lab.Models.Employees", b =>
                {
                    b.Property<string>("empId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("empName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("hireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("position_Id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("empId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Final_Lab.Models.Positions", b =>
                {
                    b.Property<string>("positionId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<float>("baseSalary")
                        .HasColumnType("float");

                    b.Property<string>("positionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("salaryIncreaseRate")
                        .HasColumnType("float");

                    b.HasKey("positionId");

                    b.ToTable("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
