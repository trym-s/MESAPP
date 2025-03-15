using System;
using System.Collections.Generic;

namespace WorkstationInfo.Entities;


public class Workorder
{
    public int WorkorderId { get; set; }
    public int WorkstationId { get; set; }
    public int TaktTime { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public string CurrentScodeValue { get; set; }

    public Workstation Workstation { get; set; }
    public ICollection<WorkstationPerformance> PerformanceRecords { get; set; }
    public ICollection<WorkorderEventLog> EventLogs { get; set; }
}
