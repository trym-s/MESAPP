using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.Workstations.GetWorkstationSummary;

public class GetWorkstationSummaryQuery : IRequest<List<WorkstationSummaryDto>> {  }