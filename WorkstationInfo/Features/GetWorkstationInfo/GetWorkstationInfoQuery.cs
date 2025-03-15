using System;
using MediatR;
using WorkstationInfo.Features.GetWorkstationInfo;

namespace WorkstationInfo.Features.GetWorkstationInfo;

public class GetWorkstationInfoQuery : IRequest<GetWorkstationInfoDto>
{
    public int WorkstationId { get; set; }

    public GetWorkstationInfoQuery
        
        (int workstationId)
    {
        WorkstationId = workstationId;
    }
}