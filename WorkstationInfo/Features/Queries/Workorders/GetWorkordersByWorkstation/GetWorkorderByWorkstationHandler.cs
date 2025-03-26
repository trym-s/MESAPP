using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;

namespace WorkstationInfo.Features.Queries.GetWorkorders.GetWorkordersByWorkstation;

public class GetWorkordersByWorkstationHandler : IRequestHandler<GetWorkordersByWorkstationQuery, List<WorkorderDto>>
{
    private readonly WorkstationInfoDbContext _context;

    public GetWorkordersByWorkstationHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkorderDto>> Handle(GetWorkordersByWorkstationQuery request, CancellationToken cancellationToken)
    {
        var workorders = await _context.Workorders
            .Where(w => w.WorkstationId == request.WorkstationId)
            .Select(w => new WorkorderDto
            {
                WorkorderId = w.WorkorderId,
                IsActive = w.IsActive,
                CurrentScodeValue = w.CurrentScodeValue,
            })
            .ToListAsync(cancellationToken);

        return workorders;
    }
}