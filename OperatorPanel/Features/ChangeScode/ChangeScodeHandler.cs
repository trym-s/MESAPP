using MediatR;
using Microsoft.EntityFrameworkCore;
using OperatorPanel.Database;
using OperatorPanel.Entities;
namespace OperatorPanel.Features.Commands.ChangeScode;
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

        int oldScode = activeWorkorder.CurrentScodeValue;

        if (oldScode == request.NewScode)
        {
            return new ChangeScodeResult
            {
                OldScode = oldScode,
                NewScode = request.NewScode,
                Message = "Scode is already set to this value."
            };
        }

        Console.WriteLine($"[DEBUG] OldScode = {oldScode}, NewScode = {request.NewScode}");

        // Güncellemeyi merkezden yap: tüm workorder'ları tara
        foreach (var wo in workstation.Workorders)
        {
            if (wo.IsActive)
            {
                wo.CurrentScodeValue = request.NewScode;
                Console.WriteLine($"[DEBUG] UPDATED ACTIVE: WorkorderId={wo.WorkorderId}, NewScode={wo.CurrentScodeValue}");
            }
            else
            {
                wo.CurrentScodeValue = 10;
                Console.WriteLine($"[DEBUG] RESET INACTIVE: WorkorderId={wo.WorkorderId}, NewScode=10");
            }
        }

        // Log oluştur
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

        // EF takip durumlarını yaz (debug için çok değerli)
        var tracked = _context.ChangeTracker.Entries<Workorder>().ToList();
        foreach (var e in tracked)
        {
            Console.WriteLine($"[TRACKED] Entity={e.Entity.WorkorderId}, State={e.State}, Scode={e.Entity.CurrentScodeValue}");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new ChangeScodeResult
        {
            OldScode = oldScode,
            NewScode = request.NewScode,
            Message = "Scode updated successfully."
        };
    }
}
