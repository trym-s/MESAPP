﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkstationInfo.Database;

#nullable disable

namespace WorkstationInfo.Migrations
{
    [DbContext(typeof(WorkstationInfoDbContext))]
    partial class WorkstationInfoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WorkstationInfo.Entities.Sensor", b =>
                {
                    b.Property<int>("SensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SensorId"));

                    b.Property<string>("SensorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("WorkstationId")
                        .HasColumnType("integer");

                    b.HasKey("SensorId");

                    b.HasIndex("WorkstationId");

                    b.ToTable("sensor", "mes_db");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Workorder", b =>
                {
                    b.Property<int>("WorkorderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WorkorderId"));

                    b.Property<int>("CurrentScodeValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TaktTime")
                        .HasColumnType("integer");

                    b.Property<int>("WorkstationId")
                        .HasColumnType("integer");

                    b.HasKey("WorkorderId");

                    b.HasIndex("IsActive");

                    b.HasIndex("WorkstationId");

                    b.ToTable("workorder", "mes_db");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.WorkorderPerformanceLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Availability")
                        .HasPrecision(8, 4)
                        .HasColumnType("numeric(8,4)");

                    b.Property<decimal>("CycleTime")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("Oee")
                        .HasPrecision(8, 4)
                        .HasColumnType("numeric(8,4)");

                    b.Property<decimal>("Performance")
                        .HasPrecision(8, 4)
                        .HasColumnType("numeric(8,4)");

                    b.Property<decimal>("Quality")
                        .HasPrecision(8, 4)
                        .HasColumnType("numeric(8,4)");

                    b.Property<DateTime>("RecordedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("TotalTime")
                        .HasColumnType("interval");

                    b.Property<int>("WorkorderId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkstationId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan?>("total_net_available_time")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("total_net_operation_time")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("total_planned_downtime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("total_startup_downtime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("total_unplanned_downtime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("WorkorderId");

                    b.HasIndex("WorkstationId");

                    b.ToTable("workorder_performance_log", "mes_db");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.WorkorderStateLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LogId"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ChangedByOperatorId")
                        .HasColumnType("integer");

                    b.Property<int>("NewScodeId")
                        .HasColumnType("integer");

                    b.Property<int>("OldScodeId")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("WorkstationId")
                        .HasColumnType("integer");

                    b.HasKey("LogId");

                    b.HasIndex("WorkstationId");

                    b.ToTable("workorder_state_log", "mes_db");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Workstation", b =>
                {
                    b.Property<int>("WorkstationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WorkstationId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkstationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("WorkstationId");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.ToTable("Workstations");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Sensor", b =>
                {
                    b.HasOne("WorkstationInfo.Entities.Workstation", "Workstation")
                        .WithMany("Sensors")
                        .HasForeignKey("WorkstationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workstation");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Workorder", b =>
                {
                    b.HasOne("WorkstationInfo.Entities.Workstation", "Workstation")
                        .WithMany("Workorders")
                        .HasForeignKey("WorkstationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workstation");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.WorkorderPerformanceLog", b =>
                {
                    b.HasOne("WorkstationInfo.Entities.Workorder", "Workorder")
                        .WithMany("PerformanceRecords")
                        .HasForeignKey("WorkorderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkstationInfo.Entities.Workstation", "Workstation")
                        .WithMany("PerformanceRecords")
                        .HasForeignKey("WorkstationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workorder");

                    b.Navigation("Workstation");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.WorkorderStateLog", b =>
                {
                    b.HasOne("WorkstationInfo.Entities.Workstation", "Workstation")
                        .WithMany("StateLogs")
                        .HasForeignKey("WorkstationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workstation");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Workorder", b =>
                {
                    b.Navigation("PerformanceRecords");
                });

            modelBuilder.Entity("WorkstationInfo.Entities.Workstation", b =>
                {
                    b.Navigation("PerformanceRecords");

                    b.Navigation("Sensors");

                    b.Navigation("StateLogs");

                    b.Navigation("Workorders");
                });
#pragma warning restore 612, 618
        }
    }
}
