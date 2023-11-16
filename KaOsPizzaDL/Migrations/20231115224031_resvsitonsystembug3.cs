using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class resvsitonsystembug3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "ReservationSystemTable",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "ReservationSystemTable",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "ReservationSystemTable");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ReservationSystemTable",
                newName: "DateTime");
        }
    }
}
