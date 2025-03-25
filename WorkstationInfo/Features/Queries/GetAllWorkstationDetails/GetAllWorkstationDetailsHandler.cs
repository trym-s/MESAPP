using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;
using WorkstationInfo.Features.Shared;

namespace WorkstationInfo.Features.Queries.GetAllWorkstationDetails;

public class GetAllWorkstationDetailsHandler : IRequestHandler<GetAllWorkstationDetailsQuery, List<WorkstationDetailsListItemDto>>
{
    private readonly WorkstationInfoDbContext _context;

    public GetAllWorkstationDetailsHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkstationDetailsListItemDto>> Handle(GetAllWorkstationDetailsQuery request, CancellationToken cancellationToken)
    {
        var workstations = await _context.Workstations
            .AsNoTracking()
            .Include(w => w.Sensors)
            .Include(w => w.Workorders)
            .Include(w => w.PerformanceRecords)
            .ToListAsync(cancellationToken);

        return workstations.Select(w =>
        {
            var activeWorkorder = w.Workorders.FirstOrDefault(wo => wo.IsActive);
            var scode = activeWorkorder?.CurrentScodeValue;
            var latestPerf = w.PerformanceRecords
                .OrderByDescending(p => p.RecordedAt)
                .FirstOrDefault();

            return new WorkstationDetailsListItemDto
            {
                WorkstationName = w.WorkstationName,
                SerialNumber = w.SerialNumber,
                ActiveScode = scode,
                Sensors = w.Sensors.Select(s => new SensorDto
                {
                    SensorId = s.SensorId,
                    SensorName = s.SensorName
                }).ToList(),
                ActiveWorkorderId = activeWorkorder?.WorkorderId,
                Oee = latestPerf?.Oee,
                Performance = latestPerf?.Performance,
                Quality = latestPerf?.Quality,
                Availability = latestPerf?.Availability,
                TotalTime = latestPerf?.TotalTime,
                CycleTime = latestPerf?.CycleTime,

                // Yeni eklenen süre alanları
                TotalStartupDowntime = latestPerf?.TotalStartupDowntime,
                TotalPlannedDowntime = latestPerf?.TotalPlannedDowntime,
                TotalUnplannedDowntime = latestPerf?.TotalUnplannedDowntime,
                TotalNetAvailableTime = latestPerf?.TotalNetAvailableTime,
                TotalNetOperationTime = latestPerf?.TotalNetOperationTime
            };
        }).ToList();

    }
}
