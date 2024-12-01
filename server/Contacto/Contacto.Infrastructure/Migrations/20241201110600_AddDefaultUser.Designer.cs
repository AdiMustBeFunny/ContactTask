﻿// <auto-generated />
using System;
using Contacto.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Contacto.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241201110600_AddDefaultUser")]
    partial class AddDefaultUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Contacto.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("ContactCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactSubCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomContactCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactCategoryId");

                    b.HasIndex("ContactSubCategoryId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.ContactCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CustomCategory")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactCategories");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.ContactSubCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ContactSubCategories");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.Contact", b =>
                {
                    b.HasOne("Contacto.Domain.Entities.ContactCategory", "ContactCategory")
                        .WithMany()
                        .HasForeignKey("ContactCategoryId");

                    b.HasOne("Contacto.Domain.Entities.ContactSubCategory", "ContactSubCategory")
                        .WithMany()
                        .HasForeignKey("ContactSubCategoryId");

                    b.Navigation("ContactCategory");

                    b.Navigation("ContactSubCategory");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.ContactSubCategory", b =>
                {
                    b.HasOne("Contacto.Domain.Entities.ContactCategory", "Category")
                        .WithMany("ContactSubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Contacto.Domain.Entities.ContactCategory", b =>
                {
                    b.Navigation("ContactSubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
