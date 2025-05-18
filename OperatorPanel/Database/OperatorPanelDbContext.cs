using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace OperatorPanel.Database;

public class OperatorPanelDbContext : DbContext
{
    public OperatorPanelDbContext(DbContextOptions<OperatorPanelDbContext> options)
        : base(options)
    {
    }

    // Shared.Entities içinden gelen DbSet tanımları (sadece kullanım için)
    public DbSet<Workorder> Workorders => Set<Workorder>();
    public DbSet<WorkorderStateLog> WorkorderStateLogs => Set<WorkorderStateLog>();
    public DbSet<Workstation> Workstations => Set<Workstation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Not: Burada Fluent API kullanımı migration üretmez, sadece model binding yapar.
        // Migration üretimi sadece MesAppDbContext’te yapılmalı

        // Amaç sadece uygulamanın run-time'da DB ile uyumlu çalışması

        modelBuilder.Entity<Workstation>().ToTable("workstation", schema: "mes_db");
        modelBuilder.Entity<Workorder>().ToTable("workorder", schema: "mes_db");
        modelBuilder.Entity<WorkorderStateLog>().ToTable("workorder_state_log", schema: "mes_db");

        // Primary Key tanımları (gerekli çünkü EF modeli tanımazsa hata verir)
        modelBuilder.Entity<Workstation>().HasKey(w => w.WorkstationId);
        modelBuilder.Entity<Workorder>().HasKey(wo => wo.WorkorderId);
        modelBuilder.Entity<WorkorderStateLog>().HasKey(log => log.LogId);

        // İlişkiler (navigasyon property'lerin düzgün çalışması için gerekli)
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
