namespace Shared.Entities;

public class SensorData
{
    public Guid Id { get; set; }                      // PRIMARY KEY
    public int WorkstationId { get; set; }            // FOREIGN KEY to workstation
    public int? WorkorderId { get; set; }              // FOREIGN KEY to workorder
    public int SensorTypeId { get; set; }             // Maps to Sensor.SensorId
    public decimal SensorValue { get; set; }          // numeric(18,4)
    public DateTime Timestamp { get; set; }           // timezone aware
}