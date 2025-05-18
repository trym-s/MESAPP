namespace Shared.Entities;


public class Workorder
{
    public int WorkorderId { get; set; }
    public int WorkstationId { get; set; }
    public int TaktTime { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public int CurrentScodeValue { get; set; }

    public required Workstation Workstation { get; set; }
    public  required ICollection<WorkorderPerformanceLog> PerformanceRecords { get; set; }
}
