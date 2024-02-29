﻿// <auto-generated />
using Foodico.Services.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Foodico.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240228212513_jfdbb")]
    partial class jfdbb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Foodico.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-10.jpg",
                            Name = "Chocolate Cream",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-1.jpg",
                            Name = "Lemon Custard",
                            Price = 13.99
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-11.jpg",
                            Name = "Chocolate Cherry",
                            Price = 10.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-12.jpg",
                            Name = "Cherry Cream",
                            Price = 16.989999999999998
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-2.jpg",
                            Name = "Chocolate Brulee",
                            Price = 21.989999999999998
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-3.jpg",
                            Name = "Double Chocolate",
                            Price = 11.99
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-4.jpg",
                            Name = "Pink Donut",
                            Price = 16.949999999999999
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-5.jpg",
                            Name = "Strawberry Mint",
                            Price = 16.949999999999999
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-6.jpg",
                            Name = "Forest Berry",
                            Price = 17.949999999999999
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-7.jpg",
                            Name = "Valentine Velvet",
                            Price = 24.949999999999999
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-8.jpg",
                            Name = "Strawberry Sprinkle",
                            Price = 24.949999999999999
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryName = "Cupcake",
                            Description = "",
                            ImageUrl = "/cake-main/img/shop/product-9.jpg",
                            Name = "Pink Cream",
                            Price = 22.949999999999999
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
