using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class reservationsytem10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationSystemTable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AM0900 = table.Column<bool>(type: "bit", nullable: false),
                    AM0930 = table.Column<bool>(type: "bit", nullable: false),
                    AM1000 = table.Column<bool>(type: "bit", nullable: false),
                    AM1030 = table.Column<bool>(type: "bit", nullable: false),
                    AM1100 = table.Column<bool>(type: "bit", nullable: false),
                    AM1130 = table.Column<bool>(type: "bit", nullable: false),
                    PM1200 = table.Column<bool>(type: "bit", nullable: false),
                    PM1230 = table.Column<bool>(type: "bit", nullable: false),
                    PM1300 = table.Column<bool>(type: "bit", nullable: false),
                    PM1330 = table.Column<bool>(type: "bit", nullable: false),
                    PM1400 = table.Column<bool>(type: "bit", nullable: false),
                    PM1430 = table.Column<bool>(type: "bit", nullable: false),
                    PM1500 = table.Column<bool>(type: "bit", nullable: false),
                    PM1530 = table.Column<bool>(type: "bit", nullable: false),
                    PM1600 = table.Column<bool>(type: "bit", nullable: false),
                    PM1630 = table.Column<bool>(type: "bit", nullable: false),
                    PM1700 = table.Column<bool>(type: "bit", nullable: false),
                    PM1730 = table.Column<bool>(type: "bit", nullable: false),
                    PM1800 = table.Column<bool>(type: "bit", nullable: false),
                    PM1830 = table.Column<bool>(type: "bit", nullable: false),
                    PM1900 = table.Column<bool>(type: "bit", nullable: false),
                    PM1930 = table.Column<bool>(type: "bit", nullable: false),
                    PM2000 = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSystemTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationSystemTable");
        }
    }
}
