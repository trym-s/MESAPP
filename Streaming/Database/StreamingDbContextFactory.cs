using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Streaming.Database
{
    public class StreamingDbContextFactory : IDesignTimeDbContextFactory<StreamingDbContext>
    {
        public StreamingDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Projeyi baz alır
                .AddJsonFile("appsettings.json") // appsettings.json'dan connectionstring okur
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StreamingDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new StreamingDbContext(optionsBuilder.Options);
        }
    }
}