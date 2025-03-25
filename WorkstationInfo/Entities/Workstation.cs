using System;
using System.Collections.Generic;

namespace WorkstationInfo.Entities;

public class Workstation
{
    public int WorkstationId { get; set; }
    public string WorkstationName { get; set; }
    public bool IsActive { get; set; }
    public string SerialNumber { get; set; }
    public string ScodeValue { get; set; }

    public ICollection<Workorder> Workorders { get; set; }
    public ICollection<Sensor> Sensors { get; set; }
    public ICollection<WorkorderPerformanceLog> PerformanceRecords { get; set; }
    public ICollection<WorkorderStateLog> StateLogs { get; set; }
}
