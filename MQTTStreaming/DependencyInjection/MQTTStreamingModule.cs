namespace MQTTStreaming.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MQTTStreaming.Database;
using MQTTStreaming.Features.SensorData.Commands.SaveSensorData;
using MQTTStreaming.Settings;

public static class StreamingModule
{
    public static IServiceCollection AddMQTTStreamingModule(this IServiceCollection services, IConfiguration configuration)
    {
        //  MQTT ayarlarını merkezi config'ten bind et
        services.Configure<MqttSettings>(configuration.GetSection("MqttSettings"));

        //  StreamingDbContext kaydı
        services.AddDbContext<MqttStreamingDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        //  Command handler kaydı (MediatR kullanıyorsan explicit tanım veya assembly taraması yapabilirsin)
        services.AddScoped<IRequestHandler<SaveSensorDataCommand, Unit>, SaveSensorDataCommandHandler>();

        //  MQTT Listener (BackgroundService)
        services.AddHostedService<MqttSensorListener>();

        return services;
    }
}
