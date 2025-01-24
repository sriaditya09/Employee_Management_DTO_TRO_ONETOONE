﻿// <auto-generated />
using Employee_Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Employee_Management.Migrations
{
    [DbContext(typeof(EmpDbContext))]
    partial class EmpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Employee_Management.Model.Entity.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company_Address")
                        .HasColumnType("text");

                    b.Property<string>("Company_Name")
                        .HasColumnType("text");

                    b.Property<int>("Pincode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Employee_Management.Model.Entity.Employees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Password")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("SalaryId")
                        .HasColumnType("integer");

                    b.Property<int?>("SalaryId1")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SalaryId")
                        .IsUnique();

                    b.HasIndex("SalaryId1")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Employee_Management.Model.Entity.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Grade")
                        .HasColumnType("text");

                    b.Property<int>("Salary_Amount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("Employee_Management.Model.Entity.Employees", b =>
                {
                    b.HasOne("Employee_Management.Model.Entity.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee_Management.Model.Entity.Salary", "Salaries")
                        .WithOne()
                        .HasForeignKey("Employee_Management.Model.Entity.Employees", "SalaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee_Management.Model.Entity.Salary", null)
                        .WithOne("Employees")
                        .HasForeignKey("Employee_Management.Model.Entity.Employees", "SalaryId1");

                    b.Navigation("Company");

                    b.Navigation("Salaries");
                });

            modelBuilder.Entity("Employee_Management.Model.Entity.Company", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Employee_Management.Model.Entity.Salary", b =>
                {
                    b.Navigation("Employees")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
