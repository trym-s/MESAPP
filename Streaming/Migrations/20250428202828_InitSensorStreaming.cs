using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streaming.Migrations
{
    /// <inheritdoc />
    public partial class InitSensorStreaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mes_db");

            migrationBuilder.CreateTable(
                name: "sensor_data",
                schema: "mes_db",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    WorkorderId = table.Column<int>(type: "integer", nullable: true),
                    SensorTypeId = table.Column<int>(type: "integer", nullable: false),
                    SensorValue = table.Column<double>(type: "double precision", precision: 18, scale: 4, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensor_data", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sensor_data",
                schema: "mes_db");
        }
    }
}
