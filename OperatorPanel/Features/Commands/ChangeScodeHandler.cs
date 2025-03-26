using MediatR;
using Microsoft.EntityFrameworkCore;
using OperatorPanel.Database;
using OperatorPanel.Entities;

namespace OperatorPanel.Features.Commands;

public class ChangeScodeHandler : IRequestHandler<ChangeScodeCommand, ChangeScodeResult>
{
    private readonly OperatorPanelDbContext _context;

    public ChangeScodeHandler(OperatorPanelDbContext context)
    {
        _context = context;
    }

    public async Task<ChangeScodeResult> Handle(ChangeScodeCommand request, CancellationToken cancellationToken)
    {
        var workstation = await _context.Workstations
            .Include(w => w.Workorders)
            .FirstOrDefaultAsync(w => w.WorkstationId == request.WorkstationId, cancellationToken);

        if (workstation == null)
            throw new KeyNotFoundException("Workstation not found.");

        if (workstation.Workorders == null || !workstation.Workorders.Any())
            throw new InvalidOperationException("No workorders found for this workstation.");

        var activeWorkorder = workstation.Workorders.FirstOrDefault(wo => wo.IsActive);

        if (activeWorkorder == null)
            throw new InvalidOperationException("No active workorder found for this workstation.");

        int oldScode = activeWorkorder.CurrentScodeValue;  // Eğer DB'de hâlâ string olarak tutuluyorsa
        //Eğer yeni scode zaten aktif olan scode ile aynıysa:
        if (oldScode == request.NewScode)
        {
            return new ChangeScodeResult
            {
                OldScode = oldScode,
                NewScode = request.NewScode,
                Message = "Scode is already set to this value." // 👈 Ek alan
            };
        }
        // Yeni scode’u güncelle
        activeWorkorder.CurrentScodeValue = request.NewScode;

        // Diğer iş emirlerini 1-0 yap (yani 10)
        foreach (var wo in workstation.Workorders)
        {
            if (wo.WorkorderId != activeWorkorder.WorkorderId)
                wo.CurrentScodeValue = 10;
        }

        var log = new WorkorderStateLog
        {
            WorkstationId = request.WorkstationId,
            WorkorderId = activeWorkorder.WorkorderId,
            OldScodeId = oldScode,
            NewScodeId = request.NewScode,
            ChangedByOperatorId = request.OperatorId,
            ChangedAt = DateTime.UtcNow,
            Reason = request.Reason
        };

        _context.WorkorderStateLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);

        return new ChangeScodeResult
        {
            OldScode = oldScode,
            NewScode = request.NewScode,
            Message = "Scode updated successfully." 
        };
    }
}
