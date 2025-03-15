using System;

namespace WorkstationInfo.Entities;

public class WorkorderEventLog
{
    public int LogId { get; set; }  
    public int WorkorderId { get; set; }
    public int WorkstationId { get; set; }
    public string ScodeValue { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Workorder Workorder { get; set; }
    public Workstation Workstation { get; set; }
}