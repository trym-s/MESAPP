using System;
using MediatR;
using WorkstationInfo.Features.GetWorkstationInfo;

namespace WorkstationInfo.Features.GetWorkstationInfo;

public class GetWorkstationDetailsQuery : IRequest<WorkstationDetailsDto>
{
    public int WorkstationId { get; set; }

    public GetWorkstationDetailsQuery
        
        (int workstationId)
    {
        WorkstationId = workstationId;
    }
}