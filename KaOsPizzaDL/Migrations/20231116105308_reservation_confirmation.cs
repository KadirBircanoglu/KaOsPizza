using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class reservation_confirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmation",
                table: "ReservationTable",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "ReservationTable");
        }
    }
}
