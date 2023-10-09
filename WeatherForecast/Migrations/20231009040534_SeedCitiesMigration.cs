using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherForecastAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtcOffset = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    UpToDate = table.Column<bool>(type: "bit", nullable: false),
                    ForecastTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryCode", "CreatedDate", "Latitude", "Longitude", "ModifiedDate", "Name", "ZipCode" },
                values: new object[,]
                {
                    { 1, "RS", new DateTime(2023, 10, 9, 4, 5, 34, 201, DateTimeKind.Utc).AddTicks(5321), 45.2551338m, 19.8451756m, new DateTime(2023, 10, 9, 4, 5, 34, 201, DateTimeKind.Utc).AddTicks(5321), "Novi Sad", "21000" },
                    { 2, "RS", new DateTime(2023, 10, 9, 4, 5, 34, 371, DateTimeKind.Utc).AddTicks(8795), 44.2708719m, 19.8863297m, new DateTime(2023, 10, 9, 4, 5, 34, 371, DateTimeKind.Utc).AddTicks(8795), "Valjevo", "14000" },
                    { 3, "RS", new DateTime(2023, 10, 9, 4, 5, 34, 467, DateTimeKind.Utc).AddTicks(5903), 45.7727397m, 19.1147036m, new DateTime(2023, 10, 9, 4, 5, 34, 467, DateTimeKind.Utc).AddTicks(5903), "Sombor", "25000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_CityId",
                table: "WeatherForecasts",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
