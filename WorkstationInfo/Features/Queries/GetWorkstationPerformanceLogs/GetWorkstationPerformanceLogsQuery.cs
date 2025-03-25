using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.GetWorkstationPerformanceLogs;

public class GetWorkstationPerformanceLogsQuery : IRequest<List<WorkstationPerformanceLogDto>>
{
    public int WorkstationId { get; set; }

    public GetWorkstationPerformanceLogsQuery(int workstationId)
    {
        WorkstationId = workstationId;
    }
}
