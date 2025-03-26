using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;
using WorkstationInfo.Features.Shared;

namespace WorkstationInfo.Features.Queries.GetWorkstationDetails;

public class GetWorkstationDetailsHandler : IRequestHandler<GetWorkstationDetailsQuery, WorkstationDetailsDto>
{
    private readonly WorkstationInfoDbContext _context;

    public GetWorkstationDetailsHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<WorkstationDetailsDto> Handle(GetWorkstationDetailsQuery request, CancellationToken cancellationToken)
    {
        var workstation = await _context.Workstations
            .Include(w => w.Sensors) // ✅ Workstation’a bağlı sensörler getiriliyor
            .Include(w => w.PerformanceRecords) // ✅ Performans kayıtları getiriliyor
            .Include(w => w.Workorders) // ✅ İş emirleri getiriliyor
            .FirstOrDefaultAsync(w => w.WorkstationId == request.WorkstationId);

        if (workstation == null)
            throw new KeyNotFoundException("Workstation not found.");

        var latestPerformance = workstation.PerformanceRecords.OrderByDescending(p => p.RecordedAt).FirstOrDefault();

        return new WorkstationDetailsDto 
        {
            WorkstationName = workstation.WorkstationName,
            SerialNumber = workstation.SerialNumber,
            Sensors = workstation.Sensors.Select(s => new SensorDto
            {
                SensorId = s.SensorId,
                SensorName = s.SensorName
            }).ToList(),
            ActiveWorkorderId = workstation.Workorders.FirstOrDefault(w => w.IsActive)?.WorkorderId,
            Oee = latestPerformance?.Oee,
            Performance = latestPerformance?.Performance,
            Quality = latestPerformance?.Quality,
            Availability = latestPerformance?.Availability,
            TotalTime = latestPerformance?.TotalTime,
            CycleTime = latestPerformance?.CycleTime,

            TotalStartupDowntime = latestPerformance?.total_startup_downtime,
            TotalPlannedDowntime = latestPerformance?.total_planned_downtime,
            TotalUnplannedDowntime = latestPerformance?.total_unplanned_downtime,
            TotalNetAvailableTime = latestPerformance?.total_net_available_time,
            TotalNetOperationTime = latestPerformance?.total_net_operation_time
        };
    }
}