namespace Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly MesAppDbContext _context;

    public UnitOfWork(MesAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}