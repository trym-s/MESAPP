using System;
using System.Collections.Generic;

namespace WorkstationInfo.Features.GetWorkstationInfo;

public class GetWorkstationInfoDto
{
    public string WorkstationName { get; set; }
    public string SerialNumber { get; set; }
    public string ActiveScode { get; set; }
    public List<SensorDto> Sensors { get; set; }
    public int? ActiveWorkorderId { get; set; }
    public decimal? Oee { get; set; }
    public decimal? Performance { get; set; }
    public decimal? Quality { get; set; }
    public decimal? Availability { get; set; }
    public TimeSpan? TotalTime { get; set; }
    public decimal? CycleTime { get; set; }

}