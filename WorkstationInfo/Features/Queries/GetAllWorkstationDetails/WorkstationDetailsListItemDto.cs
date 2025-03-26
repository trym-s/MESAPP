using System;
using System.Collections.Generic;
using WorkstationInfo.Features.Shared;

namespace WorkstationInfo.Features.Queries.GetAllWorkstationDetails;

public class WorkstationDetailsListItemDto
{
    public string? WorkstationName { get; set; }
    public string? SerialNumber { get; set; }
    public int? ActiveScode { get; set; }
    public List<SensorDto>? Sensors { get; set; }
    public int? ActiveWorkorderId { get; set; }
    public decimal? Oee { get; set; }
    public decimal? Performance { get; set; }
    public decimal? Quality { get; set; }
    public decimal? Availability { get; set; }
    public decimal? CycleTime { get; set; }
    public TimeSpan? TotalTime { get; set; }
    public TimeSpan? TotalStartupDowntime { get; set; }
    public TimeSpan? TotalPlannedDowntime { get; set; }
    public TimeSpan? TotalUnplannedDowntime { get; set; }
    public TimeSpan? TotalNetAvailableTime { get; set; }
    public TimeSpan? TotalNetOperationTime { get; set; }
}