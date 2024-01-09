﻿// <auto-generated />
using System;
using Inventory.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inventory.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240105235758_addImageUrlToLabSupply")]
    partial class addImageUrlToLabSupply
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Inventory.Models.Models.LabSupply", b =>
                {
                    b.Property<int>("SupplyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplyID"), 1L, 1);

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityOnHand")
                        .HasColumnType("int");

                    b.Property<int>("ReorderPoint")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<string>("SupplyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplyID");

                    b.HasIndex("SupplierID");

                    b.ToTable("LabSupplies");
                });

            modelBuilder.Entity("Inventory.Models.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityOrdered")
                        .HasColumnType("int");

                    b.Property<int>("SupplyID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("SupplyID");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("Inventory.Models.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierID"), 1L, 1);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Inventory.Models.Models.LabSupply", b =>
                {
                    b.HasOne("Inventory.Models.Models.Supplier", "Supplier")
                        .WithMany("LabSupplies")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Inventory.Models.Models.PurchaseOrder", b =>
                {
                    b.HasOne("Inventory.Models.Models.LabSupply", "LabSupply")
                        .WithMany()
                        .HasForeignKey("SupplyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LabSupply");
                });

            modelBuilder.Entity("Inventory.Models.Models.Supplier", b =>
                {
                    b.Navigation("LabSupplies");
                });
#pragma warning restore 612, 618
        }
    }
}
