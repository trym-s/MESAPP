using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MQTTStreaming.Database;


public class MQTTStreamingDbContextFactory : IDesignTimeDbContextFactory<MqttStreamingDbContext>
{
    public MqttStreamingDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json") // veya doğrudan override edilmiş bir Shared dosya
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<MqttStreamingDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new MqttStreamingDbContext(optionsBuilder.Options);
    }
}
