using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkstationInfo.Migrations
{
    /// <inheritdoc />
    public partial class OnlyEventLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mes_db");

            migrationBuilder.CreateTable(
                name: "Workstations",
                columns: table => new
                {
                    WorkstationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workstations", x => x.WorkstationId);
                });

            migrationBuilder.CreateTable(
                name: "sensor",
                schema: "mes_db",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    SensorName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensor", x => x.SensorId);
                    table.ForeignKey(
                        name: "FK_sensor_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workorder",
                schema: "mes_db",
                columns: table => new
                {
                    WorkorderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    TaktTime = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentScodeValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workorder", x => x.WorkorderId);
                    table.ForeignKey(
                        name: "FK_workorder_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workorder_state_log",
                schema: "mes_db",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    OldScodeId = table.Column<int>(type: "integer", nullable: false),
                    NewScodeId = table.Column<int>(type: "integer", nullable: false),
                    ChangedByOperatorId = table.Column<int>(type: "integer", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workorder_state_log", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_workorder_state_log_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workorder_performance_log",
                schema: "mes_db",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    WorkorderId = table.Column<int>(type: "integer", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Oee = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    Performance = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    Quality = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    Availability = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    CycleTime = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    total_startup_downtime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    total_planned_downtime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    total_unplanned_downtime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    total_net_available_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    total_net_operation_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    TotalTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workorder_performance_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workorder_performance_log_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workorder_performance_log_workorder_WorkorderId",
                        column: x => x.WorkorderId,
                        principalSchema: "mes_db",
                        principalTable: "workorder",
                        principalColumn: "WorkorderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sensor_WorkstationId",
                schema: "mes_db",
                table: "sensor",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_workorder_IsActive",
                schema: "mes_db",
                table: "workorder",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_workorder_WorkstationId",
                schema: "mes_db",
                table: "workorder",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_workorder_performance_log_WorkorderId",
                schema: "mes_db",
                table: "workorder_performance_log",
                column: "WorkorderId");

            migrationBuilder.CreateIndex(
                name: "IX_workorder_performance_log_WorkstationId",
                schema: "mes_db",
                table: "workorder_performance_log",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_workorder_state_log_WorkstationId",
                schema: "mes_db",
                table: "workorder_state_log",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Workstations_SerialNumber",
                table: "Workstations",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sensor",
                schema: "mes_db");

            migrationBuilder.DropTable(
                name: "workorder_performance_log",
                schema: "mes_db");

            migrationBuilder.DropTable(
                name: "workorder_state_log",
                schema: "mes_db");

            migrationBuilder.DropTable(
                name: "workorder",
                schema: "mes_db");

            migrationBuilder.DropTable(
                name: "Workstations");
        }
    }
}
