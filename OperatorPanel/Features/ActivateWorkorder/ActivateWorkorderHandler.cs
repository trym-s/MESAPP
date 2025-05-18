using MediatR;
using Microsoft.EntityFrameworkCore;
using OperatorPanel.Database;
using Shared.Entities;

namespace OperatorPanel.Features.Commands.ActivateWorkorder;

public class ActivateWorkorderHandler : IRequestHandler<ActivateWorkorderCommand, ActivateWorkorderResult>
{
    private readonly OperatorPanelDbContext _context;

    public ActivateWorkorderHandler(OperatorPanelDbContext context)
    {
        _context = context;
    }

    public async Task<ActivateWorkorderResult> Handle(ActivateWorkorderCommand request, CancellationToken cancellationToken)
    {
        var workstation = await _context.Workstations
            .Include(w => w.Workorders)
            .FirstOrDefaultAsync(w => w.WorkstationId == request.WorkstationId, cancellationToken);

        if (workstation == null)
            throw new KeyNotFoundException("Workstation not found.");
        
        var targetWorkorder = workstation.Workorders.FirstOrDefault(w => w.WorkorderId == request.WorkorderId);
        if (targetWorkorder == null)
            return new ActivateWorkorderResult
            {
                Success = false,
                Message = "Belirtilen workorder bu workstation'a ait değil."
            };
        var workorders = workstation.Workorders;
        if (workorders == null || !workorders.Any())
            throw new InvalidOperationException("No workorders found for this workstation.");

        var previousActive = workorders.FirstOrDefault(w => w.IsActive);

        foreach (var wo in workorders)
        {
            wo.IsActive = wo.WorkorderId == request.WorkorderId;
            wo.CurrentScodeValue = wo.IsActive ? request.InitialScode : 10; // 10 = Not Working
        }

        // Log entry
        var log = new WorkorderStateLog
        {
            WorkstationId = request.WorkstationId,
            WorkorderId = request.WorkorderId,
            OldScodeId = previousActive?.CurrentScodeValue ?? 10,
            NewScodeId = request.InitialScode,
            ChangedAt = DateTime.UtcNow,
            ChangedByOperatorId = request.OperatorId,
            Reason = request.Reason
        };

        _context.WorkorderStateLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);

        return new ActivateWorkorderResult
        {
            Message = "Workorder Değştirildi",
            Success = true,
            PreviousActiveWorkorderId = previousActive?.WorkorderId ?? -1,
            NewActiveWorkorderId = request.WorkorderId,
            InitialScode = request.InitialScode
        };
    }
}