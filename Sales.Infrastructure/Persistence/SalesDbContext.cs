using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Sales.Infrastructure.Persistence.Mappings;

namespace Sales.Infrastructure.Persistence
{
    public class SalesDbContext : DbContext
    {

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);//scale scanning assembly instead passing mapping in method
        }
    }
}


