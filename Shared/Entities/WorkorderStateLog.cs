namespace Shared.Entities;

public class WorkorderStateLog
{
    public int LogId { get; set; }
    public int WorkstationId { get; set; }
    public int OldScodeId { get; set; }
    public int NewScodeId { get; set; }
    public  int ChangedByOperatorId { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? Reason { get; set; }
    public Workstation Workstation { get; set; } = null!;
    public int WorkorderId { get; set; }
    public Workorder Workorder { get; set; } = null!;

}