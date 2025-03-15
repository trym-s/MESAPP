using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Entities;
using WorkstationInfo.Database;
using Infrastructure.Database;

namespace WorkstationInfo.Repositories;

public class WorkstationRepository : GenericRepository<Workstation, WorkstationInfoDbContext>, IWorkstationRepository
{
    private readonly WorkstationInfoDbContext _context;

    public WorkstationRepository(WorkstationInfoDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Workstation?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Workstations
            .Include(w => w.Workorders) // ✅ Workstation’a bağlı tüm iş emirleri
            .Include(w => w.Sensors) // ✅ Workstation’a bağlı sensörler
            .Include(w => w.PerformanceRecords.OrderByDescending(p => p.RecordedAt).Take(1)) // ✅ En güncel performans kaydı
            .FirstOrDefaultAsync(w => w.WorkstationId == id);
    }
}