using Foodico.Services.OrderAPI.Models;
using Foodico.Services.OrderAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Foodico.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartDto>().HasNoKey();
        }
    }
}
