namespace WorkstationInfo.Entities;

public class Sensor
{
    public int SensorId { get; set; }
    public int WorkstationId { get; set; }
    public string SensorName { get; set; }

    public Workstation Workstation { get; set; }
}