using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.GetWorkstationStateLogs;

public class GetWorkstationStateLogsQuery : IRequest<List<WorkstationStateLogsDto>>
{
    public int WorkstationId { get; set; }

    public GetWorkstationStateLogsQuery(int workstationId)
    {
        WorkstationId = workstationId;
    }
}