﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    [DbContext(typeof(LoncotesLibraryDbContext))]
    partial class LoncotesLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Library.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckedOutSince")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("PatronId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PatronId");

                    b.ToTable("Checkouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckedOutSince = new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 1,
                            PatronId = 1
                        },
                        new
                        {
                            Id = 2,
                            CheckedOutSince = new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 2,
                            PatronId = 2
                        },
                        new
                        {
                            Id = 3,
                            CheckedOutSince = new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 3,
                            PatronId = 3
                        });
                });

            modelBuilder.Entity("Library.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Non-Fiction"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Music"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Silly Putty"
                        });
                });

            modelBuilder.Entity("Library.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OutOfCirculationSince")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 1,
                            MaterialName = "Dracula",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 2,
                            MaterialName = "E.T",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 4,
                            MaterialName = "Silly Putty",
                            MaterialTypeId = 4
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 3,
                            MaterialName = "Paranoid",
                            MaterialTypeId = 3,
                            OutOfCirculationSince = new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Library.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DaysCheckedOut")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DaysCheckedOut = 6,
                            Name = "Book"
                        },
                        new
                        {
                            Id = 2,
                            DaysCheckedOut = 10,
                            Name = "DVD"
                        },
                        new
                        {
                            Id = 3,
                            DaysCheckedOut = 3,
                            Name = "Micofiche"
                        },
                        new
                        {
                            Id = 4,
                            DaysCheckedOut = 21,
                            Name = "Putty"
                        });
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patrons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Cave in the Woods",
                            Email = "patron1@email.com",
                            FirstName = "Horror",
                            IsActive = true,
                            LastName = "Story"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Cabin in the Woods",
                            Email = "patron2@email.com",
                            FirstName = "Gloria",
                            IsActive = true,
                            LastName = "Hood"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Pond in the Woods",
                            Email = "patron3@email.com",
                            FirstName = "Steve",
                            IsActive = false,
                            LastName = "Tub"
                        });
                });

            modelBuilder.Entity("Library.Models.Checkout", b =>
                {
                    b.HasOne("Library.Models.Material", "Material")
                        .WithMany("Checkouts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Models.Patron", "Patron")
                        .WithMany()
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("Library.Models.Material", b =>
                {
                    b.HasOne("Library.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("Library.Models.Material", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
