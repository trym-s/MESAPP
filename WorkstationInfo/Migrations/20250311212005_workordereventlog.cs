using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkstationInfo.Migrations
{
    /// <inheritdoc />
    public partial class workordereventlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Workstations_WorkstationId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkorderEventLogs_Workorders_WorkorderId",
                table: "WorkorderEventLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkorderEventLogs_Workstations_WorkstationId",
                table: "WorkorderEventLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Workorders_Workstations_WorkstationId",
                table: "Workorders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkstationPerformances_Workorders_WorkorderId",
                table: "WorkstationPerformances");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkstationPerformances_Workstations_WorkstationId",
                table: "WorkstationPerformances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workstations",
                table: "Workstations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkstationPerformances",
                table: "WorkstationPerformances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workorders",
                table: "Workorders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkorderEventLogs",
                table: "WorkorderEventLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors");

            migrationBuilder.EnsureSchema(
                name: "mes_db");

            migrationBuilder.RenameTable(
                name: "Workstations",
                newName: "workstation",
                newSchema: "mes_db");

            migrationBuilder.RenameTable(
                name: "WorkstationPerformances",
                newName: "workstation_performance",
                newSchema: "mes_db");

            migrationBuilder.RenameTable(
                name: "Workorders",
                newName: "workorder",
                newSchema: "mes_db");

            migrationBuilder.RenameTable(
                name: "WorkorderEventLogs",
                newName: "workorder_event_log",
                newSchema: "mes_db");

            migrationBuilder.RenameTable(
                name: "Sensors",
                newName: "sensor",
                newSchema: "mes_db");

            migrationBuilder.RenameIndex(
                name: "IX_WorkstationPerformances_WorkstationId",
                schema: "mes_db",
                table: "workstation_performance",
                newName: "IX_workstation_performance_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkstationPerformances_WorkorderId",
                schema: "mes_db",
                table: "workstation_performance",
                newName: "IX_workstation_performance_WorkorderId");

            migrationBuilder.RenameIndex(
                name: "IX_Workorders_WorkstationId",
                schema: "mes_db",
                table: "workorder",
                newName: "IX_workorder_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkorderEventLogs_WorkstationId",
                schema: "mes_db",
                table: "workorder_event_log",
                newName: "IX_workorder_event_log_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkorderEventLogs_WorkorderId",
                schema: "mes_db",
                table: "workorder_event_log",
                newName: "IX_workorder_event_log_WorkorderId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_WorkstationId",
                schema: "mes_db",
                table: "sensor",
                newName: "IX_sensor_WorkstationId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkstationName",
                schema: "mes_db",
                table: "workstation",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quality",
                schema: "mes_db",
                table: "workstation_performance",
                type: "numeric(8,4)",
                precision: 8,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Performance",
                schema: "mes_db",
                table: "workstation_performance",
                type: "numeric(8,4)",
                precision: 8,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Oee",
                schema: "mes_db",
                table: "workstation_performance",
                type: "numeric(8,4)",
                precision: 8,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "CycleTime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Availability",
                schema: "mes_db",
                table: "workstation_performance",
                type: "numeric(8,4)",
                precision: 8,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "SensorName",
                schema: "mes_db",
                table: "sensor",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workstation",
                schema: "mes_db",
                table: "workstation",
                column: "WorkstationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workstation_performance",
                schema: "mes_db",
                table: "workstation_performance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workorder",
                schema: "mes_db",
                table: "workorder",
                column: "WorkorderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workorder_event_log",
                schema: "mes_db",
                table: "workorder_event_log",
                column: "LogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sensor",
                schema: "mes_db",
                table: "sensor",
                column: "SensorId");

            migrationBuilder.CreateTable(
                name: "workstation_state_log",
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
                    table.PrimaryKey("PK_workstation_state_log", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_workstation_state_log_workstation_WorkstationId",
                        column: x => x.WorkstationId,
                        principalSchema: "mes_db",
                        principalTable: "workstation",
                        principalColumn: "WorkstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_workstation_SerialNumber",
                schema: "mes_db",
                table: "workstation",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_workorder_IsActive",
                schema: "mes_db",
                table: "workorder",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_workstation_state_log_WorkstationId",
                schema: "mes_db",
                table: "workstation_state_log",
                column: "WorkstationId");

            migrationBuilder.AddForeignKey(
                name: "FK_sensor_workstation_WorkstationId",
                schema: "mes_db",
                table: "sensor",
                column: "WorkstationId",
                principalSchema: "mes_db",
                principalTable: "workstation",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workorder_workstation_WorkstationId",
                schema: "mes_db",
                table: "workorder",
                column: "WorkstationId",
                principalSchema: "mes_db",
                principalTable: "workstation",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workorder_event_log_workorder_WorkorderId",
                schema: "mes_db",
                table: "workorder_event_log",
                column: "WorkorderId",
                principalSchema: "mes_db",
                principalTable: "workorder",
                principalColumn: "WorkorderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workorder_event_log_workstation_WorkstationId",
                schema: "mes_db",
                table: "workorder_event_log",
                column: "WorkstationId",
                principalSchema: "mes_db",
                principalTable: "workstation",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workstation_performance_workorder_WorkorderId",
                schema: "mes_db",
                table: "workstation_performance",
                column: "WorkorderId",
                principalSchema: "mes_db",
                principalTable: "workorder",
                principalColumn: "WorkorderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workstation_performance_workstation_WorkstationId",
                schema: "mes_db",
                table: "workstation_performance",
                column: "WorkstationId",
                principalSchema: "mes_db",
                principalTable: "workstation",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sensor_workstation_WorkstationId",
                schema: "mes_db",
                table: "sensor");

            migrationBuilder.DropForeignKey(
                name: "FK_workorder_workstation_WorkstationId",
                schema: "mes_db",
                table: "workorder");

            migrationBuilder.DropForeignKey(
                name: "FK_workorder_event_log_workorder_WorkorderId",
                schema: "mes_db",
                table: "workorder_event_log");

            migrationBuilder.DropForeignKey(
                name: "FK_workorder_event_log_workstation_WorkstationId",
                schema: "mes_db",
                table: "workorder_event_log");

            migrationBuilder.DropForeignKey(
                name: "FK_workstation_performance_workorder_WorkorderId",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropForeignKey(
                name: "FK_workstation_performance_workstation_WorkstationId",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropTable(
                name: "workstation_state_log",
                schema: "mes_db");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workstation_performance",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workstation",
                schema: "mes_db",
                table: "workstation");

            migrationBuilder.DropIndex(
                name: "IX_workstation_SerialNumber",
                schema: "mes_db",
                table: "workstation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workorder_event_log",
                schema: "mes_db",
                table: "workorder_event_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workorder",
                schema: "mes_db",
                table: "workorder");

            migrationBuilder.DropIndex(
                name: "IX_workorder_IsActive",
                schema: "mes_db",
                table: "workorder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sensor",
                schema: "mes_db",
                table: "sensor");

            migrationBuilder.RenameTable(
                name: "workstation_performance",
                schema: "mes_db",
                newName: "WorkstationPerformances");

            migrationBuilder.RenameTable(
                name: "workstation",
                schema: "mes_db",
                newName: "Workstations");

            migrationBuilder.RenameTable(
                name: "workorder_event_log",
                schema: "mes_db",
                newName: "WorkorderEventLogs");

            migrationBuilder.RenameTable(
                name: "workorder",
                schema: "mes_db",
                newName: "Workorders");

            migrationBuilder.RenameTable(
                name: "sensor",
                schema: "mes_db",
                newName: "Sensors");

            migrationBuilder.RenameIndex(
                name: "IX_workstation_performance_WorkstationId",
                table: "WorkstationPerformances",
                newName: "IX_WorkstationPerformances_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_workstation_performance_WorkorderId",
                table: "WorkstationPerformances",
                newName: "IX_WorkstationPerformances_WorkorderId");

            migrationBuilder.RenameIndex(
                name: "IX_workorder_event_log_WorkstationId",
                table: "WorkorderEventLogs",
                newName: "IX_WorkorderEventLogs_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_workorder_event_log_WorkorderId",
                table: "WorkorderEventLogs",
                newName: "IX_WorkorderEventLogs_WorkorderId");

            migrationBuilder.RenameIndex(
                name: "IX_workorder_WorkstationId",
                table: "Workorders",
                newName: "IX_Workorders_WorkstationId");

            migrationBuilder.RenameIndex(
                name: "IX_sensor_WorkstationId",
                table: "Sensors",
                newName: "IX_Sensors_WorkstationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quality",
                table: "WorkstationPerformances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,4)",
                oldPrecision: 8,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Performance",
                table: "WorkstationPerformances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,4)",
                oldPrecision: 8,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Oee",
                table: "WorkstationPerformances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,4)",
                oldPrecision: 8,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CycleTime",
                table: "WorkstationPerformances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Availability",
                table: "WorkstationPerformances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(8,4)",
                oldPrecision: 8,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "WorkstationName",
                table: "Workstations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SensorName",
                table: "Sensors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkstationPerformances",
                table: "WorkstationPerformances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workstations",
                table: "Workstations",
                column: "WorkstationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkorderEventLogs",
                table: "WorkorderEventLogs",
                column: "LogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workorders",
                table: "Workorders",
                column: "WorkorderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Workstations_WorkstationId",
                table: "Sensors",
                column: "WorkstationId",
                principalTable: "Workstations",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkorderEventLogs_Workorders_WorkorderId",
                table: "WorkorderEventLogs",
                column: "WorkorderId",
                principalTable: "Workorders",
                principalColumn: "WorkorderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkorderEventLogs_Workstations_WorkstationId",
                table: "WorkorderEventLogs",
                column: "WorkstationId",
                principalTable: "Workstations",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workorders_Workstations_WorkstationId",
                table: "Workorders",
                column: "WorkstationId",
                principalTable: "Workstations",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkstationPerformances_Workorders_WorkorderId",
                table: "WorkstationPerformances",
                column: "WorkorderId",
                principalTable: "Workorders",
                principalColumn: "WorkorderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkstationPerformances_Workstations_WorkstationId",
                table: "WorkstationPerformances",
                column: "WorkstationId",
                principalTable: "Workstations",
                principalColumn: "WorkstationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
