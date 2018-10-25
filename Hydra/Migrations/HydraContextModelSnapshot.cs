﻿// <auto-generated />
using System;
using Hydra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hydra.Migrations
{
    [DbContext(typeof(HydraContext))]
    partial class HydraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hydra.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ProductID");

                    b.Property<string>("PublisherID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Hydra.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Hydra.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductID");

                    b.Property<int>("Quantity");

                    b.Property<int?>("StoreID");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.HasIndex("StoreID");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Hydra.Models.Store", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClosingHour");

                    b.Property<double>("Latitude");

                    b.Property<double>("Lontitude");

                    b.Property<string>("Name");

                    b.Property<string>("OpeningHour");

                    b.HasKey("ID");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("Hydra.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Hydra.Models.Comment", b =>
                {
                    b.HasOne("Hydra.Models.Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductID");

                    b.HasOne("Hydra.Models.User", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherID");
                });

            modelBuilder.Entity("Hydra.Models.Stock", b =>
                {
                    b.HasOne("Hydra.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Hydra.Models.Store")
                        .WithMany("Stock")
                        .HasForeignKey("StoreID");
                });
#pragma warning restore 612, 618
        }
    }
}
