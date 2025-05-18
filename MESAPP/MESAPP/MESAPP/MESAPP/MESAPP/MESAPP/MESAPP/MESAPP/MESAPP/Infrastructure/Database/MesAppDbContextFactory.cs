using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database
{
    public class MesAppDbContextFactory : IDesignTimeDbContextFactory<MesAppDbContext>
    {
        public MesAppDbContext CreateDbContext(string[] args)
        {
            // Load configuration manually
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing in appsettings.json.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<MesAppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MesAppDbContext(optionsBuilder.Options);
        }
    }
}