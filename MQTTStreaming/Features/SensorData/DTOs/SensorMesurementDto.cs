namespace MQTTStreaming.Features.SensorData.DTOs;

public class SensorMeasurementDto
{
    public SensorMeasurementDto() { } 

    public int? SensorTypeId { get; set; }
    public string? SensorName { get; set; }
    public decimal Value { get; set; }
}
