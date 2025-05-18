namespace MQTTStreaming.Features.SensorData.DTOs;

public class SensorDataPayloadDto
{
    public int WorkstationId { get; set; }
    public int? WorkorderId { get; set; }
    public DateTime Timestamp { get; set; }
    public required List<SensorMeasurementDto> Measurements { get; set; }
}