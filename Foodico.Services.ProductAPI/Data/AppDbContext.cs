using Foodico.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodico.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Chocolate Cream",
                Price = 15,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-10.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Lemon Custard",
                Price = 13.99,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-1.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Chocolate Cherry",
                Price = 10.99,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-11.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Cherry Cream",
                Price = 16.99,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-12.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Chocolate Brulee",
                Price = 21.99,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-2.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Double Chocolate",
                Price = 11.99,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-3.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 7,
                Name = "Pink Donut",
                Price = 16.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-4.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 8,
                Name = "Strawberry Mint",
                Price = 16.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-5.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 9,
                Name = "Forest Berry",
                Price = 17.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-6.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 10,
                Name = "Valentine Velvet",
                Price = 24.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-7.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 11,
                Name = "Strawberry Sprinkle",
                Price = 24.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-8.jpg",
                CategoryName = "Cupcake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 12,
                Name = "Pink Cream",
                Price = 22.95,
                Description = "",
                ImageUrl = "/cake-main/img/shop/product-9.jpg",
                CategoryName = "Cupcake"
            });
           
        }
    }
}
