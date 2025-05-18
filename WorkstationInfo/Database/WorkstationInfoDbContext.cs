using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace WorkstationInfo.Database;

public class WorkstationInfoDbContext : DbContext
{
    public WorkstationInfoDbContext(DbContextOptions<WorkstationInfoDbContext> options)
        : base(options) { }

    public DbSet<Workstation> Workstations => Set<Workstation>();
    public DbSet<Workorder> Workorders => Set<Workorder>();
    public DbSet<WorkorderPerformanceLog> WorkorderPerformanceLogs => Set<WorkorderPerformanceLog>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<WorkorderStateLog> WorkorderStateLogs => Set<WorkorderStateLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tablo eşleştirmeleri (Shared.Entities ile aynı olmalı)
        modelBuilder.Entity<Workstation>().ToTable("workstation", "mes_db");
        modelBuilder.Entity<Workorder>().ToTable("workorder", "mes_db");
        modelBuilder.Entity<WorkorderPerformanceLog>().ToTable("workorder_performance_log", "mes_db");
        modelBuilder.Entity<Sensor>().ToTable("sensor", "mes_db");
        modelBuilder.Entity<WorkorderStateLog>().ToTable("workorder_state_log", "mes_db");

        // Primary key'ler
        modelBuilder.Entity<Workstation>().HasKey(w => w.WorkstationId);
        modelBuilder.Entity<Workorder>().HasKey(wo => wo.WorkorderId);
        modelBuilder.Entity<WorkorderPerformanceLog>().HasKey(p => p.Id);
        modelBuilder.Entity<Sensor>().HasKey(s => s.SensorId);
        modelBuilder.Entity<WorkorderStateLog>().HasKey(log => log.LogId);

        // İlişkiler
        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.Workorders)
            .WithOne(wo => wo.Workstation)
            .HasForeignKey(wo => wo.WorkstationId);

        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.Sensors)
            .WithOne(s => s.Workstation)
            .HasForeignKey(s => s.WorkstationId);

        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.PerformanceRecords)
            .WithOne(p => p.Workstation)
            .HasForeignKey(p => p.WorkstationId);

        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.StateLogs)
            .WithOne(sl => sl.Workstation)
            .HasForeignKey(sl => sl.WorkstationId);

        modelBuilder.Entity<Workorder>()
            .HasMany(wo => wo.PerformanceRecords)
            .WithOne(p => p.Workorder)
            .HasForeignKey(p => p.WorkorderId);

        // Özellik konfigürasyonları
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Oee).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Performance).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Quality).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.Availability).HasPrecision(8, 4);
        modelBuilder.Entity<WorkorderPerformanceLog>().Property(p => p.CycleTime).HasPrecision(10, 2);

        modelBuilder.Entity<Workstation>().Property(w => w.WorkstationName).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Sensor>().Property(s => s.SensorName).HasMaxLength(50).IsRequired();

        // Indexler
        modelBuilder.Entity<Workstation>().HasIndex(w => w.SerialNumber).IsUnique();
        modelBuilder.Entity<Workorder>().HasIndex(wo => wo.IsActive);
    }
}
