using System.Collections.Generic;
using MediatR;
using WorkstationInfo.Features.Queries.GetWorkstationDetails;

namespace WorkstationInfo.Features.Queries.GetAllWorkstationDetails;

public class GetAllWorkstationDetailsQuery :  IRequest<List<WorkstationDetailsListItemDto>>  { }
