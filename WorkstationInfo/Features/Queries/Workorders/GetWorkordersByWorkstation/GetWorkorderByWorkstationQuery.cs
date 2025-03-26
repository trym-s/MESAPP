using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.GetWorkorders.GetWorkordersByWorkstation;

public class GetWorkordersByWorkstationQuery : IRequest<List<WorkorderDto>>
{
    public int WorkstationId { get; set; }

    public GetWorkordersByWorkstationQuery(int workstationId)
    {
        WorkstationId = workstationId;
    }
}
