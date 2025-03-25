using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkstationInfo.Migrations
{
    /// <inheritdoc />
    public partial class AddTimingFieldsToWorkstationPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalNetAvailableTime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalNetOperationTime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalPlannedDowntime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalStartupDowntime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalUnplannedDowntime",
                schema: "mes_db",
                table: "workstation_performance",
                type: "interval",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalNetAvailableTime",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropColumn(
                name: "TotalNetOperationTime",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropColumn(
                name: "TotalPlannedDowntime",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropColumn(
                name: "TotalStartupDowntime",
                schema: "mes_db",
                table: "workstation_performance");

            migrationBuilder.DropColumn(
                name: "TotalUnplannedDowntime",
                schema: "mes_db",
                table: "workstation_performance");
        }
    }
}
