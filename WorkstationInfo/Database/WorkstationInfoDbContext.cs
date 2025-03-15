using Microsoft.EntityFrameworkCore;
using WorkstationInfo.Entities;

namespace WorkstationInfo.Database;

public class WorkstationInfoDbContext : DbContext
{
    public WorkstationInfoDbContext(DbContextOptions<WorkstationInfoDbContext> options)
        : base(options) { }

    public DbSet<Workstation> Workstations { get; set; }
    public DbSet<Workorder> Workorders { get; set; }
    public DbSet<WorkstationPerformance> WorkstationPerformances { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<WorkorderEventLog> WorkorderEventLogs { get; set; }
    public DbSet<WorkstationStateLog> WorkstationStateLogs { get; set; } // <— Include this too!

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ------------------------------------------------------
        //  TABLE MAPPINGS
        // ------------------------------------------------------
        modelBuilder.Entity<Workstation>()
            .ToTable("workstation", schema: "mes_db");
        
        modelBuilder.Entity<Workorder>()
            .ToTable("workorder", schema: "mes_db");
        
        modelBuilder.Entity<WorkstationPerformance>()
            .ToTable("workstation_performance", schema: "mes_db");
        
        modelBuilder.Entity<Sensor>()
            .ToTable("sensor", schema: "mes_db");
        
        modelBuilder.Entity<WorkorderEventLog>()
            .ToTable("workorder_event_log", schema: "mes_db");
        
        modelBuilder.Entity<WorkstationStateLog>()
            .ToTable("workstation_state_log", schema: "mes_db");

        // ------------------------------------------------------
        //  PRIMARY KEYS (EF usually infers from "Id"/"EntityNameId", 
        //  but being explicit can be clearer)
        // ------------------------------------------------------
        modelBuilder.Entity<Workstation>()
            .HasKey(w => w.WorkstationId);

        modelBuilder.Entity<Workorder>()
            .HasKey(wo => wo.WorkorderId);

        modelBuilder.Entity<WorkstationPerformance>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Sensor>()
            .HasKey(s => s.SensorId);

        modelBuilder.Entity<WorkorderEventLog>()
            .HasKey(log => log.LogId);

        modelBuilder.Entity<WorkstationStateLog>()
            .HasKey(log => log.LogId);

        // ------------------------------------------------------
        //  RELATIONSHIPS
        // ------------------------------------------------------

        // (1) Workstation -> Workorder
        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.Workorders)
            .WithOne(wo => wo.Workstation)
            .HasForeignKey(wo => wo.WorkstationId);

        // (2) Workstation -> Sensor
        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.Sensors)
            .WithOne(s => s.Workstation)
            .HasForeignKey(s => s.WorkstationId);

        // (3) Workstation -> WorkstationPerformance
        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.PerformanceRecords)
            .WithOne(p => p.Workstation)
            .HasForeignKey(p => p.WorkstationId);

        // (4) Workorder -> WorkstationPerformance
        //     If you want a direct relationship from Workorder to WorkstationPerformance
        modelBuilder.Entity<Workorder>()
            .HasMany(wo => wo.PerformanceRecords)
            .WithOne(p => p.Workorder)
            .HasForeignKey(p => p.WorkorderId);

        // (5) WorkorderEventLog -> Workorder
        modelBuilder.Entity<WorkorderEventLog>()
            .HasOne(log => log.Workorder)
            .WithMany(wo => wo.EventLogs)
            .HasForeignKey(log => log.WorkorderId);

        // (6) WorkorderEventLog -> Workstation
        modelBuilder.Entity<WorkorderEventLog>()
            .HasOne(log => log.Workstation)
            .WithMany()
            .HasForeignKey(log => log.WorkstationId);

        // (7) WorkstationStateLog -> Workstation
        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.StateLogs)
            .WithOne(sl => sl.Workstation)
            .HasForeignKey(sl => sl.WorkstationId);



        // ------------------------------------------------------
        //  ADDITIONAL PROPERTY CONFIGURATIONS
        // ------------------------------------------------------
        // e.g. Setting decimal precision
        modelBuilder.Entity<WorkstationPerformance>()
            .Property(p => p.Oee)
            .HasPrecision(8, 4); // adapt as needed

        modelBuilder.Entity<WorkstationPerformance>()
            .Property(p => p.Performance)
            .HasPrecision(8, 4);

        modelBuilder.Entity<WorkstationPerformance>()
            .Property(p => p.Quality)
            .HasPrecision(8, 4);

        modelBuilder.Entity<WorkstationPerformance>()
            .Property(p => p.Availability)
            .HasPrecision(8, 4);

        modelBuilder.Entity<WorkstationPerformance>()
            .Property(p => p.CycleTime)
            .HasPrecision(10, 2);

        // e.g. Setting string length constraints
        modelBuilder.Entity<Workstation>()
            .Property(w => w.WorkstationName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Sensor>()
            .Property(s => s.SensorName)
            .HasMaxLength(50)
            .IsRequired();

        // etc.

        // ------------------------------------------------------
        // (OPTIONAL) INDEXES, UNIQUENESS, ETC.
        // ------------------------------------------------------
        // Example: If you want unique SerialNumber
        modelBuilder.Entity<Workstation>()
            .HasIndex(w => w.SerialNumber)
            .IsUnique();

        // Example: Index on frequently filtered columns
        modelBuilder.Entity<Workorder>()
            .HasIndex(wo => wo.IsActive);

        // Repeat as needed...
    }
}