using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;

namespace WorkstationInfo.Features.GetWorkstationInfo;

public class GetWorkstationInfoHandler : IRequestHandler<GetWorkstationInfoQuery, GetWorkstationInfoDto>
{
    private readonly WorkstationInfoDbContext _context;

    public GetWorkstationInfoHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<GetWorkstationInfoDto> Handle(GetWorkstationInfoQuery request, CancellationToken cancellationToken)
    {
        var workstation = await _context.Workstations
            .Include(w => w.Sensors) // ✅ Workstation’a bağlı sensörler getiriliyor
            .Include(w => w.PerformanceRecords) // ✅ Performans kayıtları getiriliyor
            .Include(w => w.Workorders) // ✅ İş emirleri getiriliyor
            .FirstOrDefaultAsync(w => w.WorkstationId == request.WorkstationId);

        if (workstation == null)
            throw new KeyNotFoundException("Workstation not found.");

        var latestPerformance = workstation.PerformanceRecords.OrderByDescending(p => p.RecordedAt).FirstOrDefault();

        return new GetWorkstationInfoDto 
        {
            WorkstationName = workstation.WorkstationName,
            SerialNumber = workstation.SerialNumber,
            ActiveScode = workstation.ScodeValue,
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
            CycleTime = latestPerformance?.CycleTime
        };
    }
}