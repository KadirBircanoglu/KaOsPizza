using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class resvsitonsystembug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AM0900",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "AM0930",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "AM1000",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "AM1030",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "AM1100",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "AM1130",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1200",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1230",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1300",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1330",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1400",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1430",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1500",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1530",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1600",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1630",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1700",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1730",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1800",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1830",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1900",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM1930",
                table: "ReservationSystemTable");

            migrationBuilder.DropColumn(
                name: "PM2000",
                table: "ReservationSystemTable");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ReservationSystemTable",
                newName: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "ReservationSystemTable",
                newName: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "AM0900",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AM0930",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AM1000",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AM1030",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AM1100",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AM1130",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1200",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1230",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1300",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1330",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1400",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1430",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1500",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1530",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1600",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1630",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1700",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1730",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1800",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1830",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1900",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM1930",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PM2000",
                table: "ReservationSystemTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
