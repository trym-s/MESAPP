namespace MQTTStreaming.Settings;

public class MqttSettings
{
    public string Broker { get; set; } = default!;
    public int Port { get; set; } = 1883;
    public string? Username { get; set; }
    public string? Password { get; set; }
}