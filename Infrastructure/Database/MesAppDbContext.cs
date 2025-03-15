using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database; 
    
public class MesAppDbContext : DbContext
{
    public MesAppDbContext(DbContextOptions<MesAppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MesAppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}