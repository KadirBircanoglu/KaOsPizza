using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaOsPizzaDL.Migrations
{
    /// <inheritdoc />
    public partial class userPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "AspNetUsers");
        }
    }
}
