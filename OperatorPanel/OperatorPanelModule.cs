using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OperatorPanel.Database;

namespace OperatorPanel;

public static class OperatorPanelModule
{
    public static IServiceCollection AddOperatorPanelModule(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<OperatorPanelDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // MediatR kayıtları
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}