using System;

namespace WorkstationInfo.Entities;

public class WorkstationStateLog
{
    public int LogId { get; set; }
    public int WorkstationId { get; set; }
    public int OldScodeId { get; set; }
    public int NewScodeId { get; set; }
    public  int ChangedByOperatorId { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? Reason { get; set; }  
    public Workstation Workstation { get; set; }
}