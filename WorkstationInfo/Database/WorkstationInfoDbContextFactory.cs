using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WorkstationInfo.Database;

public class WorkstationInfoDbContextFactory : IDesignTimeDbContextFactory<WorkstationInfoDbContext>
{
    public WorkstationInfoDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<WorkstationInfoDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new WorkstationInfoDbContext(optionsBuilder.Options);
    }
}