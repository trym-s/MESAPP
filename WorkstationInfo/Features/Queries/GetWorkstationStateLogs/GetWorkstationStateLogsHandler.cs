using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;

namespace WorkstationInfo.Features.Queries.GetWorkstationStateLogs;

public class GetWorkstationStateLogsHandler : IRequestHandler<GetWorkstationStateLogsQuery, List<WorkstationStateLogsDto>>
{
    private readonly WorkstationInfoDbContext _context;
    private IRequestHandler<GetWorkstationStateLogsQuery, List<WorkstationStateLogsDto>> _requestHandlerImplementation;

    public GetWorkstationStateLogsHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkstationStateLogsDto>> Handle(GetWorkstationStateLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _context.WorkorderStateLogs
            .Where(log => log.WorkstationId == request.WorkstationId)
            .OrderByDescending(log => log.ChangedAt)
            .ToListAsync(cancellationToken);

        var relatedWorkorders = await _context.Workorders
            .Where(w => w.WorkstationId == request.WorkstationId)
            .ToListAsync(cancellationToken);

        var result = logs.Select(log =>
        {
            var relatedOrder = relatedWorkorders
                .FirstOrDefault(w => log.ChangedAt >= w.StartDate && log.ChangedAt <= w.FinishDate);

            return new WorkstationStateLogsDto
            {
                LogId = log.LogId,
                WorkorderId = relatedOrder?.WorkorderId ?? 0,
                OldScodeId = log.OldScodeId,
                NewScodeId = log.NewScodeId,
                ChangedByOperatorId = log.ChangedByOperatorId,
                ChangedAt = log.ChangedAt,
                Reason = log.Reason,
                WorkorderStartDate = relatedOrder?.StartDate ?? DateTime.MinValue,
                WorkorderFinishDate = relatedOrder?.FinishDate ?? DateTime.MinValue
            };
        }).ToList();

        return result;
    }
}

