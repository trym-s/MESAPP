using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Infrastructure.Database;

public class MesAppDbContext : DbContext
{
    public MesAppDbContext(DbContextOptions<MesAppDbContext> options)
        : base(options)
    {
    }

    // ---------- DbSet Tanımları (SharedKernel’den) ----------
    public DbSet<Workorder> Workorders { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<SensorData> SensorData { get; set; }
    public DbSet<SensorType> SensorTypes { get; set; }
    public DbSet<WorkorderPerformanceLog> WorkorderPerformanceLogs { get; set; }
    public DbSet<WorkorderStateLog> WorkorderStateLogs { get; set; }

    // ---------- Model Configuration ----------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tabloların schema ile eşleştirilmesi
        modelBuilder.Entity<Workstation>().ToTable("workstation", schema: "mes_db");
        modelBuilder.Entity<Workorder>().ToTable("workorder", schema: "mes_db");
        modelBuilder.Entity<Sensor>().ToTable("sensor", schema: "mes_db");
        modelBuilder.Entity<SensorData>().ToTable("sensor_data", schema: "mes_db");
        modelBuilder.Entity<SensorType>().ToTable("sensor_types", schema: "mes_db");
        modelBuilder.Entity<WorkorderPerformanceLog>().ToTable("workorder_performance_log", schema: "mes_db");
        modelBuilder.Entity<WorkorderStateLog>().ToTable("workorder_state_log", schema: "mes_db");

        // Eğer EntityTypeConfiguration dosyaları kullanıyorsan uncomment et:
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(MesAppDbContext).Assembly);

        // Precision örnekleri (isteğe göre)
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Oee).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Performance).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Quality).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Availability).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.CycleTime).HasPrecision(10, 2);
    }
}
