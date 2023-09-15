﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PruebaAltiora.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ClientEntity", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ClientId");

                    b.ToTable("client", (string)null);
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("OrderProductEntity", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("order_product", (string)null);
                });

            modelBuilder.Entity("ProductEntity", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("ProductId");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.HasOne("ClientEntity", "ClientEntity")
                        .WithMany("OrderEntities")
                        .HasForeignKey("ClientId");

                    b.Navigation("ClientEntity");
                });

            modelBuilder.Entity("OrderProductEntity", b =>
                {
                    b.HasOne("OrderEntity", "OrderEntity")
                        .WithMany("OrderProductEntities")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ProductEntity", "ProductEntity")
                        .WithMany("OrderProductEntities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("OrderEntity");

                    b.Navigation("ProductEntity");
                });

            modelBuilder.Entity("ClientEntity", b =>
                {
                    b.Navigation("OrderEntities");
                });

            modelBuilder.Entity("OrderEntity", b =>
                {
                    b.Navigation("OrderProductEntities");
                });

            modelBuilder.Entity("ProductEntity", b =>
                {
                    b.Navigation("OrderProductEntities");
                });
#pragma warning restore 612, 618
        }
    }
}