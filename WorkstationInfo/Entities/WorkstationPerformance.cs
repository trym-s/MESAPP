using System;
using System.Collections.Generic;

namespace WorkstationInfo.Entities;

public class WorkstationPerformance
{
    public int Id { get; set; }
    public int WorkstationId { get; set; }
    public int WorkorderId { get; set; }
    public DateTime RecordedAt { get; set; }
    public decimal Oee { get; set; }
    public decimal Performance { get; set; }
    public decimal Quality { get; set; }
    public decimal Availability { get; set; }
    public TimeSpan TotalTime { get; set; }
    public decimal CycleTime { get; set; }

    public Workstation Workstation { get; set; }
    public Workorder Workorder { get; set; }
}
