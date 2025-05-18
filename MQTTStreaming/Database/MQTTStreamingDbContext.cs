using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace MQTTStreaming.Database;

public class MqttStreamingDbContext(DbContextOptions<MqttStreamingDbContext> options)
    : DbContext(options)
{
    public DbSet<SensorData> SensorData => Set<SensorData>();
    public DbSet<SensorType> SensorTypes => Set<SensorType>();
    public DbSet<Workorder> Workorders => Set<Workorder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // SENSOR DATA
        modelBuilder.Entity<SensorData>(entity =>
        {
            entity.ToTable("sensor_data", "mes_db");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.SensorTypeId).HasColumnName("sensor_type_id");
            entity.Property(e => e.SensorValue).HasColumnName("sensor_value").HasPrecision(18, 4);
            entity.Property(e => e.Timestamp).HasColumnName("timestamp").HasColumnType("timestamptz");
            entity.Property(e => e.WorkorderId).HasColumnName("workorder_id");
            entity.Property(e => e.WorkstationId).HasColumnName("workstation_id");
        });

        // SENSOR TYPE
        modelBuilder.Entity<SensorType>(entity =>
        {
            entity.ToTable("sensor_types", "mes_db");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Unit).HasColumnName("unit").HasMaxLength(50);
            entity.Property(e => e.DataType).HasColumnName("data_type").HasMaxLength(50).IsRequired();
            entity.Property(e => e.MinValue).HasColumnName("min_value").HasPrecision(18, 4);
            entity.Property(e => e.MaxValue).HasColumnName("max_value").HasPrecision(18, 4);
            entity.Property(e => e.Description).HasColumnName("description");
        });

        // WORKORDER
        modelBuilder.Entity<Workorder>(entity =>
        {
            entity.ToTable("workorder", "mes_db");

            entity.HasKey(e => e.WorkorderId);

            entity.Property(e => e.WorkorderId).HasColumnName("WorkorderId");
            entity.Property(e => e.WorkstationId).HasColumnName("WorkstationId");
            entity.Property(e => e.TaktTime).HasColumnName("TaktTime");
            entity.Property(e => e.Quantity).HasColumnName("Quantity");
            entity.Property(e => e.IsActive).HasColumnName("IsActive");
            entity.Property(e => e.StartDate).HasColumnName("StartDate");
            entity.Property(e => e.FinishDate).HasColumnName("FinishDate");
            entity.Property(e => e.CurrentScodeValue).HasColumnName("CurrentScodeValue");
        });
    }
}
