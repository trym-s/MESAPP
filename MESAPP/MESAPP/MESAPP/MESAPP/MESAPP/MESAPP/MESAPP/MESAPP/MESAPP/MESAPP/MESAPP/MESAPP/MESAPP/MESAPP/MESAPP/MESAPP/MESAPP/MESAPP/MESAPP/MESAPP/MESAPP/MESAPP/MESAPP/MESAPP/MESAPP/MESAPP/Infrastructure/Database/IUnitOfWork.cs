namespace Infrastructure.Database;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}