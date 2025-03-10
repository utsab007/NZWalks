﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalks.API.Data;

#nullable disable

namespace NZWalks.API.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    [Migration("20250309130906_seeding data for Difficulties and Regions")]
    partial class seedingdataforDifficultiesandRegions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a2f36d39-fc69-4464-aed6-79a3c8f62eb9"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("f718d1e2-40c7-4b34-bfc5-488b694c4b19"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("331238bb-bc64-447c-b29d-21b9998ec3cd"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f1b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                            Code = "NOR",
                            Name = "Northland",
                            RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/northland/northland-landscape-2.jpg"
                        },
                        new
                        {
                            Id = new Guid("f2b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                            Code = "ACK",
                            Name = "Auckland",
                            RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/auckland/auckland-landscape-1.jpg"
                        },
                        new
                        {
                            Id = new Guid("f3b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                            Code = "WAI",
                            Name = "Waikato",
                            RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/waikato/waikato-landscape-1.jpg"
                        },
                        new
                        {
                            Id = new Guid("f4b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                            Code = "BOP",
                            Name = "Bay of Plenty",
                            RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/bay-of-plenty/bay-of-plenty-landscape-1.jpg"
                        },
                        new
                        {
                            Id = new Guid("f5b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                            Code = "TAR",
                            Name = "Taranaki",
                            RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/taranaki/taranaki-landscape-1.jpg"
                        });
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("NZWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
