using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sales.Infrastructure.Persistence;
using System.IO;

public class SalesDbContextFactory
    : IDesignTimeDbContextFactory<SalesDbContext>
{
    public SalesDbContext CreateDbContext(string[] args)
    {
        // Adjust base path to point to API project
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build();

        var connectionString = configuration.GetConnectionString("SalesDatabase");

        var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new SalesDbContext(optionsBuilder.Options);
    }
}