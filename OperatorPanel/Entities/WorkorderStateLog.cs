namespace OperatorPanel.Entities;

public class WorkorderStateLog
{
    public int LogId { get; set; }
    public int WorkstationId { get; set; }
    public int WorkorderId { get; set; } // ✅ Yeni eklendi
    public int OldScodeId { get; set; }
    public int NewScodeId { get; set; }
    public int ChangedByOperatorId { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? Reason { get; set; }

    public Workstation Workstation { get; set; } = default!;
    public Workorder Workorder { get; set; } = default!;
} 
