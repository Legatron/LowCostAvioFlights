using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LowCostAvioFlights.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchHashCodeToFlightSearchParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the new column to the existing table
            migrationBuilder.AddColumn<string>(
                name: "SearchHashCode",
                table: "FlightSearchParameters",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightSearchParameters");
        }
    }
}
