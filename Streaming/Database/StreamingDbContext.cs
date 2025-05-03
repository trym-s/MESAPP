using Microsoft.EntityFrameworkCore;
using Streaming.Entities;

namespace Streaming.Database
{
    public class StreamingDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=upeys;Username=postgres;Password=111;",
                    o => o.MigrationsHistoryTable("__SensorStreamingMigrationsHistory", "mes_db"));
            }
        }

        public StreamingDbContext(DbContextOptions<StreamingDbContext> options)
            : base(options)
        {
        }

        public DbSet<SensorData> SensorDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.ToTable("sensor_data", "mes_db");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id") 
                    .IsRequired();

                entity.Property(e => e.WorkstationId)
                    .HasColumnName("workstation_id")
                    .IsRequired();

                entity.Property(e => e.WorkorderId)
                    .HasColumnName("workorder_id")
                    .IsRequired(false);

                entity.Property(e => e.SensorTypeId)
                    .HasColumnName("sensor_type_id")
                    .IsRequired();

                entity.Property(e => e.SensorValue)
                    .HasColumnName("sensor_value")
                    .IsRequired()
                    .HasPrecision(18, 4);

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .IsRequired();
            });

        }
    }
}