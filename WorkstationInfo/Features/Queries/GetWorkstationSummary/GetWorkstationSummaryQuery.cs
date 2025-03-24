using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Queries.GetWorkstationSummary;

public class GetWorkstationSummaryQuery : IRequest<List<WorkstationSummaryDto>> {  }