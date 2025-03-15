using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkstationInfo.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkorderEventLogKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workstations",
                columns: table => new
                {
                    WorkstationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    ScodeValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workstations", x => x.WorkstationId);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    SensorName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.SensorId);
                    table.ForeignKey(
                        name: "FK_Sensors_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workorders",
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
                    CurrentScodeValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workorders", x => x.WorkorderId);
                    table.ForeignKey(
                        name: "FK_Workorders_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkorderEventLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkorderId = table.Column<int>(type: "integer", nullable: false),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    ScodeValue = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkorderEventLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_WorkorderEventLogs_Workorders_WorkorderId",
                        column: x => x.WorkorderId,
                        principalTable: "Workorders",
                        principalColumn: "WorkorderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkorderEventLogs_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkstationPerformances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    WorkorderId = table.Column<int>(type: "integer", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Oee = table.Column<decimal>(type: "numeric", nullable: false),
                    Performance = table.Column<decimal>(type: "numeric", nullable: false),
                    Quality = table.Column<decimal>(type: "numeric", nullable: false),
                    Availability = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CycleTime = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkstationPerformances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkstationPerformances_Workorders_WorkorderId",
                        column: x => x.WorkorderId,
                        principalTable: "Workorders",
                        principalColumn: "WorkorderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkstationPerformances_Workstations_WorkstationId",
                        column: x => x.WorkstationId,
                        principalTable: "Workstations",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_WorkstationId",
                table: "Sensors",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkorderEventLogs_WorkorderId",
                table: "WorkorderEventLogs",
                column: "WorkorderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkorderEventLogs_WorkstationId",
                table: "WorkorderEventLogs",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Workorders_WorkstationId",
                table: "Workorders",
                column: "WorkstationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkstationPerformances_WorkorderId",
                table: "WorkstationPerformances",
                column: "WorkorderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkstationPerformances_WorkstationId",
                table: "WorkstationPerformances",
                column: "WorkstationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "WorkorderEventLogs");

            migrationBuilder.DropTable(
                name: "WorkstationPerformances");

            migrationBuilder.DropTable(
                name: "Workorders");

            migrationBuilder.DropTable(
                name: "Workstations");
        }
    }
}
