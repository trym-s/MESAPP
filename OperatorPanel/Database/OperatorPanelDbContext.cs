using Microsoft.EntityFrameworkCore;
using OperatorPanel.Entities;

namespace OperatorPanel.Database;

public class OperatorPanelDbContext : DbContext
{
    public OperatorPanelDbContext(DbContextOptions<OperatorPanelDbContext> options)
        : base(options)
    {
    }

    public DbSet<Workorder> Workorders { get; set; }
    public DbSet<WorkorderStateLog> WorkorderStateLogs { get; set; }
    public DbSet<Workstation> Workstations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------- TABLE MAPPINGS ----------
        modelBuilder.Entity<Workstation>()
            .ToTable("workstation", schema: "mes_db");

        modelBuilder.Entity<Workorder>()
            .ToTable("workorder", schema: "mes_db");

        modelBuilder.Entity<WorkorderStateLog>()
            .ToTable("workorder_state_log", schema: "mes_db");

        // ---------- PRIMARY KEYS ----------
        modelBuilder.Entity<Workstation>()
            .HasKey(w => w.WorkstationId);

        modelBuilder.Entity<Workorder>()
            .HasKey(wo => wo.WorkorderId);

        modelBuilder.Entity<WorkorderStateLog>()
            .HasKey(log => log.LogId);

        // ---------- RELATIONSHIPS ----------

        modelBuilder.Entity<Workstation>()
            .HasMany(w => w.Workorders)
            .WithOne(wo => wo.Workstation)
            .HasForeignKey(wo => wo.WorkstationId);

        modelBuilder.Entity<WorkorderStateLog>()
            .HasOne(log => log.Workstation)
            .WithMany()
            .HasForeignKey(log => log.WorkstationId);

        modelBuilder.Entity<WorkorderStateLog>()
            .HasOne(log => log.Workorder)
            .WithMany()
            .HasForeignKey(log => log.WorkorderId);
    }
}