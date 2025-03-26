using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;

namespace WorkstationInfo.Features.Queries.GetWorkstationPerformanceLogs;

public class GetWorkstationPerformanceLogsHandler : IRequestHandler<GetWorkstationPerformanceLogsQuery, List<WorkstationPerformanceLogDto>>
{
    private readonly WorkstationInfoDbContext _context;

    public GetWorkstationPerformanceLogsHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkstationPerformanceLogDto>> Handle(GetWorkstationPerformanceLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _context.WorkorderPerformanceLogs
            .Where(p => p.WorkstationId == request.WorkstationId)
            .OrderByDescending(p => p.RecordedAt)
            .Select(p => new WorkstationPerformanceLogDto
            {
                RecordedAt = p.RecordedAt,
                Oee = p.Oee,
                Availability = p.Availability,
                Performance = p.Performance,
                Quality = p.Quality,
                TotalTime = p.TotalTime,
                TotalNetOperationTime = p.total_net_operation_time,
                WorkorderId = p.WorkorderId
            })
            .ToListAsync(cancellationToken);

        return logs;
    }
}