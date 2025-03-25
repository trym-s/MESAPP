using System;

namespace WorkstationInfo.Features.Queries.GetWorkstationPerformanceLogs;

public class WorkstationPerformanceLogDto
{
    public DateTime RecordedAt { get; set; }
    public decimal Oee { get; set; }
    public decimal Availability { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }

    public TimeSpan? TotalTime { get; set; }
    public TimeSpan? TotalNetOperationTime { get; set; }

    public int WorkorderId { get; set; }
}