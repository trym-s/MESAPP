using System.Collections.Generic;
using MediatR;

namespace WorkstationInfo.Features.Quaries.GetWorkstationSummary;

public class GetWorkstationSummaryQuery : IRequest<List<WorkstationSummaryDto>> {  }