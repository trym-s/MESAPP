using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.GetScodeDistribution;

public class GetScodeDistributionQuery : IRequest<List<ScodeDurationDto>>
{
    public int WorkstationId { get; set; }

    public GetScodeDistributionQuery(int workstationId)
    {
        WorkstationId = workstationId;
    }
}