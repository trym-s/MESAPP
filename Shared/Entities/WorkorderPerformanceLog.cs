namespace Shared.Entities;

public class WorkorderPerformanceLog
{
    public int Id { get; set; }
    public int WorkstationId { get; set; }
    public int WorkorderId { get; set; }
    public DateTime RecordedAt { get; set; }
    public decimal Oee { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal Availability { get; set; }
    public decimal CycleTime { get; set; }

    public Workstation Workstation { get; set; }
    public Workorder Workorder { get; set; }
    
    public TimeSpan? total_startup_downtime { get; set; }
    public TimeSpan? total_planned_downtime { get; set; }
    public TimeSpan? total_unplanned_downtime { get; set; }
    public TimeSpan? total_net_available_time { get; set; }
    public TimeSpan? total_net_operation_time { get; set; }
    public TimeSpan TotalTime { get; set; }
}
