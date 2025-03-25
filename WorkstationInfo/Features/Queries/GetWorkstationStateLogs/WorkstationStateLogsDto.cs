using System;

namespace WorkstationInfo.Features.Queries.GetWorkstationStateLogs;

public class WorkstationStateLogsDto
{
    public int LogId { get; set; }
    public int WorkorderId { get; set; }
    public int OldScodeId { get; set; }
    public int NewScodeId { get; set; }
    public int ChangedByOperatorId { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? Reason { get; set; }

    public DateTime WorkorderStartDate { get; set; }
    public DateTime WorkorderFinishDate { get; set; }
}