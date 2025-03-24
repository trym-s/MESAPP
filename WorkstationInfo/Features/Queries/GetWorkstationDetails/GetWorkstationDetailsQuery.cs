using MediatR;

namespace WorkstationInfo.Features.Queries.GetWorkstationDetails;

public class GetWorkstationDetailsQuery : IRequest<WorkstationDetailsDto>
{
    public int WorkstationId { get; set; }

    public GetWorkstationDetailsQuery
        
        (int workstationId)
    {
        WorkstationId = workstationId;
    }
}