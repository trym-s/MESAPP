using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class GenericRepository<T, TContext> : IGenericRepository<T>
    where T : class
    where TContext : DbContext
{
    protected readonly TContext Context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(TContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
}