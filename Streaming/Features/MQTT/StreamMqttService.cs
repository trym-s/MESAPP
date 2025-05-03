using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using Streaming.Entities;
using Streaming.Services;
using Streaming.Settings;
using Streaming.DependencyInjection.Features;
using System.Text.Json;

namespace Streaming.Features.Mqtt
{
    public class StreamMqttService : BackgroundService
    {
        private readonly MqttSettings _mqttSettings;
        private readonly SensorDataQueue _sensorDataQueue;
        private readonly IMqttClient _mqttClient;

        public StreamMqttService(IOptions<MqttSettings> mqttOptions, SensorDataQueue sensorDataQueue)
        {
            Console.WriteLine("🚀 StreamMqttService constructor çalıştı!");

            _mqttSettings = mqttOptions.Value;
            _sensorDataQueue = sensorDataQueue;

            // 🔍 Test logları:
            Console.WriteLine($"🧪 MqttSettings Test:");
            Console.WriteLine($"     Broker   : {_mqttSettings.Broker ?? "NULL"}");
            Console.WriteLine($"     Port     : {_mqttSettings.Port}");
            Console.WriteLine($"     ClientId : {_mqttSettings.ClientId ?? "NULL"}");
            Console.WriteLine($"     Topic    : {_mqttSettings.Topic ?? "NULL"}");

            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🔧 Starting ExecuteAsync...");

            try
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(_mqttSettings.Broker, _mqttSettings.Port)
                    .WithClientId(_mqttSettings.ClientId)
                    .WithCredentials(_mqttSettings.Username, _mqttSettings.Password)
                    .WithCleanSession()
                    .Build();

                _mqttClient.ConnectedAsync += async e =>
                {
                    Console.WriteLine("✅ Connected to MQTT Broker!");

                    await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                        .WithTopic(_mqttSettings.Topic)
                        .Build());

                    Console.WriteLine($"📡 Subscribed to MQTT Topic: {_mqttSettings.Topic}");
                };

                _mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    try
                    {
                        Console.WriteLine("📥 Message Received from MQTT Broker");

                        var payload = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"📥 Payload: {payload}");

                        var parsedData = JsonSerializer.Deserialize<StreamMqttMessage>(payload);

                        if (parsedData?.Sensors != null)
                        {
                            foreach (var sensor in parsedData.Sensors)
                            {
                                var sensorData = new SensorData
                                {
                                    Id = Guid.NewGuid(),
                                    WorkstationId = parsedData.WorkstationId,
                                    SensorTypeId = sensor.TypeId,
                                    SensorValue = sensor.Value,
                                    Timestamp = parsedData.Timestamp ?? DateTime.UtcNow
                                };

                                _sensorDataQueue.Enqueue(sensorData);

                                Console.WriteLine($"✅ Enqueued SensorData -> WorkstationId: {sensorData.WorkstationId}, SensorTypeId: {sensorData.SensorTypeId}, Value: {sensorData.SensorValue}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Warning: Received payload could not be parsed correctly.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Exception while processing MQTT message: {ex.Message}");
                    }
                };

                await _mqttClient.ConnectAsync(options, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception while connecting to MQTT Broker: {ex.Message}");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
