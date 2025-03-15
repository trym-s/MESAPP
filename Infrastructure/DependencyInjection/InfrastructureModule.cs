using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Infrastructure.Options;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringOptions = new ConnectionStringOptions();
        configuration.GetSection("DatabaseSettings").Bind(connectionStringOptions);
        services.AddSingleton(connectionStringOptions);

        services.AddDbContext<MesAppDbContext>((serviceProvider, options) =>
        {
            var connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
            if (string.IsNullOrEmpty(connectionOptions.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(connectionOptions.DefaultConnection), "Connection string is missing in appsettings.json.");
            }

            options.UseNpgsql(connectionOptions.DefaultConnection);
        });

        return services;
    }
}