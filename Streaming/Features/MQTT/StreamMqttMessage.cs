namespace Streaming.DependencyInjection.Features;

public class StreamMqttMessage
{
    public int WorkstationId { get; set; }
    public List<MqttSensorData> Sensors { get; set; } = new();
    public DateTime? Timestamp { get; set; }
}

public class MqttSensorData
{
    public int TypeId { get; set; }
    public double Value { get; set; }
}
    
