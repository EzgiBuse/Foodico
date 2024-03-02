using Foodico.Services.ShoppingCartAPI.Models;
using Foodico.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Foodico.Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
