using ComandasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComandasApp.Infrastructure.Persistence
{
    public class ComandasDbContext : DbContext
    {
        public ComandasDbContext(DbContextOptions<ComandasDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplica todas las configuraciones que implementen IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComandasDbContext).Assembly);
        }
    }
}
