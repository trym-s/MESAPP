using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTStreaming.Features.SensorData.Commands.SaveSensorData;
using MQTTStreaming.Features.SensorData.DTOs;
using MQTTStreaming.Settings;

namespace MQTTStreaming.Services;

public class MqttSensorListener : BackgroundService
{
    private readonly ILogger<MqttSensorListener> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly MqttSettings _settings;
    private IMqttClient _mqttClient = new MqttFactory().CreateMqttClient();

    public MqttSensorListener(ILogger<MqttSensorListener> logger, IServiceScopeFactory scopeFactory, IOptions<MqttSettings> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _settings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_settings.Broker, _settings.Port)
            .Build();

        _mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            try
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var dto = JsonSerializer.Deserialize<SensorDataPayloadDto>(payload, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dto is not null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new SaveSensorDataCommand(dto));
                }
                else
                {
                    _logger.LogWarning("MQTT mesajı deserialize edilemedi veya boş içerik geldi.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MQTT mesajı işlenemedi.");
            }
        };

        _mqttClient.ConnectedAsync += async e =>
        {
            _logger.LogInformation("MQTT bağlandı.");

            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("factory/workstation/+/sensors", MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());

            _logger.LogInformation("MQTT topic'e abone olundu.");
        };

        await _mqttClient.ConnectAsync(mqttOptions, stoppingToken);
    }
}