using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OperatorPanel.Database;

public class OperatorPanelDbContextFactory : IDesignTimeDbContextFactory<OperatorPanelDbContext>
{
    public OperatorPanelDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<OperatorPanelDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new OperatorPanelDbContext(optionsBuilder.Options);
    }
}
