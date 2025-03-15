using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkstationInfo.Database;
using WorkstationInfo.Repositories;

namespace WorkstationInfo;

public static class WorkstationInfoModule
{
    public static IServiceCollection AddWorkstationInfoModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkstationInfoDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IWorkstationRepository, WorkstationRepository>();

        return services;
    }
}