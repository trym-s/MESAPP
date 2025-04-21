using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;

namespace WorkstationInfo.Features.Queries.GetScodeDistribution;

public class GetScodeDistributionHandler : IRequestHandler<GetScodeDistributionQuery, List<ScodeDurationDto>>
{
    private readonly WorkstationInfoDbContext _context;

    public GetScodeDistributionHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<ScodeDurationDto>> Handle(GetScodeDistributionQuery request, CancellationToken cancellationToken)
    {
        var logs = await _context.WorkorderStateLogs
            .Where(log => log.WorkstationId == request.WorkstationId)
            .OrderBy(log => log.ChangedAt)
            .ToListAsync(cancellationToken);

        var scodeDurations = new Dictionary<int, double>(); // scode => durationMinutes

        for (int i = 0; i < logs.Count - 1; i++)
        {
            var current = logs[i];
            var next = logs[i + 1];

            var duration = (next.ChangedAt - current.ChangedAt).TotalMinutes;

            if (scodeDurations.ContainsKey(current.NewScodeId))
                scodeDurations[current.NewScodeId] += duration;
            else
                scodeDurations[current.NewScodeId] = duration;
        }

        // Son SCODE için bitiş zamanı yoksa hesaplama dışı bırakılabilir veya Now() kullanılabilir
        // Örn: logs.Last().NewScodeId için duration hesaplamak istiyorsan aşağıdaki kod eklenebilir:
        // var lastLog = logs.Last();
        // var duration = (DateTime.UtcNow - lastLog.ChangedAt).TotalMinutes;

        return scodeDurations
            .Select(kvp => new ScodeDurationDto
            {
                Scode = kvp.Key,
                DurationMinutes = kvp.Value
            })
            .OrderByDescending(x => x.DurationMinutes)
            .ToList();
    }
}