using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Streaming.Database;
using Streaming.Features.Mqtt;
using Streaming.Services; // SensorStreamingDbContext'ın namespace'i
using Streaming.Settings; // Settings dosyalarının namespace'i

namespace Streaming.DependencyInjection
{
    public static class StreamingModule
    {
        public static IServiceCollection AddStreamingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StreamingDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable("__SensorStreamingMigrationsHistory", "mes_db")));

            services.Configure<MqttSettings>(configuration.GetSection("MqttSettings")); // MQTT ayarlarını bağla
            services.AddSingleton<SensorDataQueue>();
            services.AddHostedService<StreamMqttService>(); // 🔥 BU SATIR MUTLAKA OLMALI

            return services;
        }
    }
}