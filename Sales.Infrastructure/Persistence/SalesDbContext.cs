using Microsoft.EntityFrameworkCore;
using Sales.Domain.Orders.Entities;

namespace Sales.Infrastructure.Persistence
{
    public class SalesDbContext : DbContext
    {

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);//scale scanning assembly instead passing mapping in method
        }
    }
}


