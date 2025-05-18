namespace Shared.Entities;

public class Sensor
{
    public int SensorId { get; set; }
    public int WorkstationId { get; set; }
    public required string SensorName { get; set; }

    public required Workstation Workstation { get; set; }
}