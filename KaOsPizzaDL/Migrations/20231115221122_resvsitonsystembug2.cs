using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class resvsitonsystembug2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "ReservationTable");

            migrationBuilder.AddColumn<long>(
                name: "ReservationSystemId",
                table: "ReservationTable",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTable_ReservationSystemId",
                table: "ReservationTable",
                column: "ReservationSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTable_ReservationSystemTable_ReservationSystemId",
                table: "ReservationTable",
                column: "ReservationSystemId",
                principalTable: "ReservationSystemTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTable_ReservationSystemTable_ReservationSystemId",
                table: "ReservationTable");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTable_ReservationSystemId",
                table: "ReservationTable");

            migrationBuilder.DropColumn(
                name: "ReservationSystemId",
                table: "ReservationTable");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "ReservationTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
