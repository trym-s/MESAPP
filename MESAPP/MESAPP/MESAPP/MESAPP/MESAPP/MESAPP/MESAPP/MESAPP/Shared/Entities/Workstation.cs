namespace Shared.Entities;

public class Workstation
{
    public int WorkstationId { get; set; }

    public required string WorkstationName { get; set; }
    public bool IsActive { get; set; }
    public required string SerialNumber { get; set; }

    public required ICollection<Workorder> Workorders { get; set; }
    public required ICollection<Sensor> Sensors { get; set; }
    public required ICollection<WorkorderPerformanceLog> PerformanceRecords { get; set; }
    public required ICollection<WorkorderStateLog> StateLogs { get; set; }
}
