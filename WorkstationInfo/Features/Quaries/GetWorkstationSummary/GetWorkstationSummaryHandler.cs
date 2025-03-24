using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Database;
using WorkstationInfo.Mappings;

namespace WorkstationInfo.Features.Quaries.GetWorkstationSummary;

public class GetWorkstationSummaryHandler : IRequestHandler<GetWorkstationSummaryQuery, List<WorkstationSummaryDto>>
{
    private readonly WorkstationInfoDbContext _context;

    public GetWorkstationSummaryHandler(WorkstationInfoDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkstationSummaryDto>> Handle(GetWorkstationSummaryQuery request, CancellationToken cancellationToken)
    {
        var workstations = await _context.Workstations
            .Include(w => w.Workorders)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return workstations.Select(w =>
        {
            var activeWorkorder = w.Workorders.FirstOrDefault(wo => wo.IsActive);
            var scode = activeWorkorder?.CurrentScodeValue;
            var (status, color) = ScodeMapper.Map(scode);

            return new WorkstationSummaryDto
            {
                Id = w.WorkstationId,
                Name = w.WorkstationName,
                SerialNumber = w.SerialNumber,
                ActiveWorkorderId = activeWorkorder?.WorkorderId,
                Status = status,
                StatusColor = color
            };
        }).ToList();
    }
}