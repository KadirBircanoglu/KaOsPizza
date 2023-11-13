using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class servisTableUrlEkleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoURL",
                table: "ServicesTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoURL",
                table: "ServicesTable");
        }
    }
}
